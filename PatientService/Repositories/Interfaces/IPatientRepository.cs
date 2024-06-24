using PatientService.Common;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;

namespace PatientService.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<AppResponse> GetAllPatients();
        Task<AppResponse> GetPatientById(int id);
        Task<AppResponse> AddPatient(PatientDTO patient);
        Task<AppResponse> UpdatePatient(PatientDTO patient,int id);
        Task<AppResponse> DeletePatient(int id);

        Task<AppResponse> ShowVaccinationStatus(int patientId);
        Task<AppResponse> UpdateVaccinationStatusByPatientId(int id);
    }
}
