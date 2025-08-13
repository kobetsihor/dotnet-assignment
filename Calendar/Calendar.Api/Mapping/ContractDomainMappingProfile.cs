using AutoMapper;
using Calendar.Contracts.Requests;
using Calendar.Contracts.Responses;
using Calendar.Domain.Models.Input;
using Calendar.Domain.Models.Output;
using VetAppointment = Calendar.Contracts.Responses.VetAppointment;

namespace Calendar.Api.Mapping
{
    public class ContractDomainMappingProfile : Profile
    {
        public ContractDomainMappingProfile()
        {
            CreateMap<CreateAnimalRequest, CreateAnimalInput>();
            CreateMap<CreateAnimalInput, CreateAnimalRequest>();
            CreateMap<CreateAnimalOutput, CreateAnimalResponse>();
            CreateMap<CreateAnimalInput, CreateAnimalResponse>();
            CreateMap<CreateAnimalOutput, CreateAnimalInput>();
            CreateMap<CreateAnimalRequest, CreateAnimalOutput>();
            CreateMap<GetAnimalOutput, CreateAnimalResponse>();
            CreateMap<GetAnimalOutput, CreateAnimalOutput>();

            CreateMap<CreateAppointmentRequest, CreateAppointmentInput>();
            CreateMap<CreateAppointmentInput, CreateAppointmentRequest>();
            CreateMap<CreateAppointmentOutput, GetAppointmentResponse>();
            CreateMap<GetAppointmentOutput, GetAppointmentResponse>();
            CreateMap<CreateAppointmentInput, CreateAppointmentOutput>();
            CreateMap<CreateAppointmentRequest, CreateAppointmentOutput>();
            CreateMap<GetAppointmentOutput, CreateAppointmentOutput>();

            CreateMap<GetVetAppointmentsOutput, GetVetAppointmentsResponse>()
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments));
            
            CreateMap<Domain.Models.Output.VetAppointment, VetAppointment>();
        }
    }
}