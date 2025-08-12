using AutoMapper;
using Calendar.Domain.Models.Input;
using Calendar.DataAccess.Entities;
using Calendar.DataAccess.Enums;
using Calendar.Domain.Models.Output;

namespace Calendar.Domain.Mapping
{
    public class DomainEntityMappingProfile : Profile
    {
        public DomainEntityMappingProfile()
        {
            CreateMap<CreateAnimalInput, Animal>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
            CreateMap<Animal, GetAnimalOutput>();
            CreateMap<Animal, CreateAnimalOutput>();

            CreateMap<CreateAppointmentInput, Appointment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => AppointmentStatus.Scheduled));
            CreateMap<Appointment, CreateAppointmentOutput>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<Appointment, GetAppointmentOutput>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Appointment, VetAppointment>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
