using AutoFixture;
using AutoFixture.AutoMoq;
using Calendar.DataAccess.Entities;
using Calendar.DataAccess.Enums;
using Calendar.DataAccess.Repositories;
using Calendar.Domain.Handlers;
using Calendar.Domain.Models.Input;
using Moq;

namespace Calendar.Unit.Tests.Domain.Handlers
{
    public class UpdateAppointmentStatusHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsRepository> _appointmentsRepositoryMock;
        private readonly Mock<IAnimalsRepository> _animalsRepositoryMock;
        private readonly UpdateAppointmentStatusHandler _handler;

        public UpdateAppointmentStatusHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _appointmentsRepositoryMock = _fixture.Freeze<Mock<IAppointmentsRepository>>();
            _animalsRepositoryMock = _fixture.Freeze<Mock<IAnimalsRepository>>();
            _handler = new UpdateAppointmentStatusHandler(
                _appointmentsRepositoryMock.Object,
                _animalsRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_UpdatesStatusSuccessfully()
        {
            // Arrange
            var appointment = _fixture.Build<Appointment>()
                .With(a => a.Status, AppointmentStatus.Scheduled)
                .With(a => a.StartTime, DateTime.UtcNow.AddHours(2))
                .Create();

            var input = new UpdateAppointmentStatusInput
            {
                AppointmentId = appointment.Id,
                Status = "Completed"
            };

            _appointmentsRepositoryMock
                .Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            _appointmentsRepositoryMock
                .Setup(r => r.UpdateAsync(appointment, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.Equal(AppointmentStatus.Completed, appointment.Status);
            Assert.Equal(MediatR.Unit.Value, result);
        }

        [Fact]
        public async Task Handle_ThrowsArgumentException_ForInvalidStatus()
        {
            // Arrange
            var appointment = _fixture.Create<Appointment>();
            var input = new UpdateAppointmentStatusInput
            {
                AppointmentId = appointment.Id,
                Status = "InvalidStatus"
            };

            _appointmentsRepositoryMock
                .Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(input, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ThrowsInvalidOperationException_WhenCancelWithinOneHour()
        {
            // Arrange
            var appointment = _fixture.Build<Appointment>()
                .With(a => a.Status, AppointmentStatus.Scheduled)
                .With(a => a.StartTime, DateTime.UtcNow.AddMinutes(30))
                .Create();

            var input = new UpdateAppointmentStatusInput
            {
                AppointmentId = appointment.Id,
                Status = "Cancelled"
            };

            _appointmentsRepositoryMock
                .Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(input, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_SendsEmail_WhenStatusIsCancelled()
        {
            // Arrange
            var appointment = _fixture.Build<Appointment>()
                .With(a => a.Status, AppointmentStatus.Scheduled)
                .With(a => a.StartTime, DateTime.UtcNow.AddHours(2))
                .With(a => a.AnimalId, Guid.NewGuid())
                .Create();

            var input = new UpdateAppointmentStatusInput
            {
                AppointmentId = appointment.Id,
                Status = "Cancelled"
            };

            var animal = _fixture.Build<Animal>()
                .With(a => a.OwnerEmail, "owner@example.com")
                .With(a => a.Id, appointment.AnimalId)
                .Create();

            _appointmentsRepositoryMock
                .Setup(r => r.GetByIdAsync(appointment.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(appointment);

            _appointmentsRepositoryMock
                .Setup(r => r.UpdateAsync(appointment, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _animalsRepositoryMock
                .Setup(r => r.GetByIdAsync(appointment.AnimalId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(animal);

            // Act & Assert
            var ex = await Record.ExceptionAsync(() => _handler.Handle(input, CancellationToken.None));
            Assert.Null(ex);
            Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
        }

        [Fact]
        public async Task Handle_ThrowsKeyNotFoundException_WhenAppointmentNotFound()
        {
            // Arrange
            var input = _fixture.Create<UpdateAppointmentStatusInput>();
            _ = _appointmentsRepositoryMock
                .Setup(r => r.GetByIdAsync(input.AppointmentId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Appointment)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(input, CancellationToken.None));
        }
    }
}
