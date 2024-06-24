using AutoMapper;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Common
{
    public class AutoMapperHandler: Profile
    {
        public AutoMapperHandler()
        {

            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Vaccination, VaccinationDTO>().ReverseMap();
           

        }
    }
}
