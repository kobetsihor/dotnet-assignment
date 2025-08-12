using AutoFixture;
using AutoFixture.AutoMoq;
using Calendar.Domain.Handlers;
using Calendar.Domain.Models.Input;
using Calendar.DataAccess.Repositories;
using Moq;
using AutoMapper;
using Calendar.Domain.Models.Output;
using Calendar.DataAccess.Entities;

namespace Calendar.Unit.Tests.Domain.Handlers
{
    public class GetVetAppointmentsHandlerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAppointmentsRepository> _appointmentsRepositoryMock;
        private readonly Mock<IAnimalsRepository> _animalsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetVetAppointmentsHandler _handler;

        public GetVetAppointmentsHandlerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _appointmentsRepositoryMock = _fixture.Freeze<Mock<IAppointmentsRepository>>();
            _animalsRepositoryMock = _fixture.Freeze<Mock<IAnimalsRepository>>();
            _mapperMock = _fixture.Freeze<Mock<IMapper>>();
            _handler = new GetVetAppointmentsHandler(
                _appointmentsRepositoryMock.Object,
                _animalsRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoAppointmentsExist()
        {
            // Arrange
            var input = _fixture.Create<GetVetAppointmentsInput>();
            var output = new GetVetAppointmentsOutput { Appointments = new List<VetAppointment>() };

            _appointmentsRepositoryMock
                .Setup(r => r.GetByVeterinarianAndDateRangeAsync(
                    input.VeterinarianId,
                    input.StartDate,
                    input.EndDate,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            _mapperMock
                .Setup(m => m.Map<GetVetAppointmentsOutput>(It.IsAny<List<Appointment>>()))
                .Returns(output);

            // Act
            var result = await _handler.Handle(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Appointments);
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryThrows()
        {
            // Arrange
            var input = _fixture.Create<GetVetAppointmentsInput>();

            _appointmentsRepositoryMock
                .Setup(r => r.GetByVeterinarianAndDateRangeAsync(
                    input.VeterinarianId,
                    input.StartDate,
                    input.EndDate,
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(input, CancellationToken.None));
        }
    }
}
