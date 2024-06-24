using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PatientService.Common;
using PatientService.Data;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;
using PatientService.Repositories.Interfaces;

namespace PatientService.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _patientDbContext;
        public PatientRepository(PatientDbContext patientDbContext)
        {
            _patientDbContext = patientDbContext;
        }
        public async Task<AppResponse> AddPatient(PatientDTO patient)
        {
            var _patient = new Patient
            {
                Name = patient.Name,
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                VaccinationStatus = Constants.NOT_VACINATED

            };

            var response = new AppResponse();
            try
            {

                await _patientDbContext.AddAsync(_patient);
                await _patientDbContext.SaveChangesAsync();
                response.Result = patient;
                response.IsSuccess = true;
                response.Message = "Patient added successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding patient: {ex.Message}";
            }
            return response;
        }


        public async Task<AppResponse> DeletePatient(int id)
        {
            var response = new AppResponse();
            try
            {
                var patient = await _patientDbContext.Patients.FindAsync(id);
                if (patient != null)
                {
                    _patientDbContext.Patients.Remove(patient);
                    await _patientDbContext.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Message = "Patient deleted successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Patient with ID '{id}' not found.";
                }
                
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error deleting patient: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> GetAllPatients()
        {
            var response = new AppResponse();
            try
            {
                IEnumerable<Patient> patients = await _patientDbContext.Patients.Include(p => p.Vaccinations).ToListAsync();
                response.Result = patients;
                response.IsSuccess = true;
                response.Message = "Patients retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving patients: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> GetPatientById(int id)
        {
            var response = new AppResponse();
            try
            {
                

                var patient = await _patientDbContext.Patients
                                            .Include(p => p.Vaccinations)
                                            .FirstOrDefaultAsync(p => p.PatientId == id);

               /* var patientDetails = await (from p in _patientDbContext.Patients
                                            where p.PatientId == id
                                            select new
                                            {
                                                p.PatientId,
                                                p.Name,
                                                p.DateOfBirth,
                                                p.Email,
                                                p.PhoneNumber,
                                                Vaccinations = p.Vaccinations.Select(v => new
                                                {
                                                    v.VaccinationId,
                                                    v.VaccineName,
                                                    v.VaccinationDate,
                                                    v.DoseNumber
                                                }).ToList()
                                            }).FirstOrDefaultAsync();
*/

                if (patient != null)
                {
                    
                    response.Result = patient;
                    response.IsSuccess = true;
                    response.Message = "Patient found.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Patient with ID '{id}' not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving patient: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> UpdatePatient(PatientDTO patient,int id)
        {
            var response = new AppResponse();
            try
            {

                var _patient = await this._patientDbContext.Patients.FindAsync(id);

                if (_patient != null)
                {
                    _patient.Name = patient.Name;
                    _patient.DateOfBirth = patient.DateOfBirth;
                    _patient.Email = patient.Email;
                    _patient.PhoneNumber = patient.PhoneNumber;
                   // _patient.VaccinationStatus = ""
                   
                    await this._patientDbContext.SaveChangesAsync();
                    response.Result = patient;
                    response.IsSuccess = true;
                    response.Message = "Patient record updated successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Patient with ID '{id}' not found.";
                }

              
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error updating patient: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> UpdateVaccinationStatusByPatientId(int id)
        {
            var response = new AppResponse();
            var _patient = await this._patientDbContext.Patients.FindAsync(id);

            if (_patient != null)
            {
                var patientVaccinationCount = await _patientDbContext.Patients
                                  .Where(p => p.PatientId == id)
                                  .Select(p => p.Vaccinations.Count)
                                  .FirstOrDefaultAsync();
               _patient.VaccinationStatus = patientVaccinationCount >= 2 ? Constants.FULLY_VACCINATED : patientVaccinationCount == 0 ?
               Constants.NOT_VACINATED : Constants.PARTIALLY_VACCINATED;
                await this._patientDbContext.SaveChangesAsync();

                await this._patientDbContext.SaveChangesAsync();
                response.Result = _patient.VaccinationStatus;
                response.IsSuccess = true;
                response.Message = "Patient status updated successfully.";
            }
            else
            {
                response.IsSuccess = false;
                response.Message = $"Patient with ID '{id}' not found.";
            }


            return response;
        }
    }
}
