using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using PatientService.Common;
using PatientService.Data;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;
using PatientService.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PatientService.Repositories.Implementations
{
    public class VaccinationRepository : IVaccinationRepository
    {
        private readonly PatientDbContext _patientDbContext;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        public VaccinationRepository(PatientDbContext patientDbContext, IPatientRepository patientRepository, IMapper mapper)
        {

            _patientDbContext = patientDbContext;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }
        public async Task<AppResponse> AddVaccination(VaccinationDTO vaccinationDTO)
        {
            var response = new AppResponse();
            


            try
            {
                var _patiendExist = await _patientDbContext.Patients.FirstOrDefaultAsync(x => x.PatientId == vaccinationDTO.PatientId);
                if (_patiendExist != null)
                {
                    var _vaccination = new Vaccination
                    {
                        VaccineName = vaccinationDTO.VaccineName,
                        VaccinationDate = vaccinationDTO.VaccinationDate,
                        DoseNumber = vaccinationDTO.DoseNumber,
                        PatientId = vaccinationDTO.PatientId,
                    };
                    await _patientDbContext.AddAsync(_vaccination);
                    await _patientDbContext.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Message = "Patient get Vaccinated successfully.";

                    // update VaccinationStatus
                    AppResponse appResponse = await _patientRepository.UpdateVaccinationStatusByPatientId(vaccinationDTO.PatientId);

                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"Patiend Does not exits with patiend Id '{vaccinationDTO?.PatientId}'";
                }




            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error adding Vaccination: {ex.Message}";
            }   
            return response;
        }

        public async Task<AppResponse> GetVaccinationById(int id)
        {
            var response = new AppResponse();
            try
            {


                var vaccinations = await _patientDbContext.Vaccinations.FirstOrDefaultAsync(v => v.VaccinationId == id);



                if (vaccinations!=null)
                {
                    var vaccinationDTO = _mapper.Map<VaccinationDTO>(vaccinations);

                    response.Result = vaccinationDTO;
                    response.IsSuccess = true;
                    response.Message = $"vaccinations found with Vaccination Id '{id}'";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"vaccinations not found with ID '{id}'.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving vaccinations: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> GetVaccinationsByPatientId(int patientId)
        {
            var response = new AppResponse();
            try
            {


                var vaccinations = await _patientDbContext.Vaccinations.Where(v => v.PatientId == patientId).ToListAsync();
                
                

                if (vaccinations.Count>0)
                {
                    var vaccinationDTO = _mapper.Map<List<VaccinationDTO>>(vaccinations);

                    response.Result = vaccinationDTO;
                    response.IsSuccess = true;
                    response.Message = $"vaccinations found of patiend Id '{patientId}'";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"vaccinations with patiend ID '{patientId}' not found.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving vaccinations: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse> UpdateVacination(VaccinationDTO vaccinationDTO, int id)
        {
            var response = new AppResponse();
            try
            {


                var vaccination = await _patientDbContext.Vaccinations.FirstOrDefaultAsync(v => v.VaccinationId == id);



                if (vaccination != null)
                {
                    vaccination.VaccineName = vaccinationDTO.VaccineName;
                    vaccination.DoseNumber = vaccination.DoseNumber;
                    vaccination.VaccinationDate = vaccinationDTO.VaccinationDate;

                    await this._patientDbContext.SaveChangesAsync();
                    response.Result = vaccinationDTO;
                    response.IsSuccess = true;
                    response.Message = $"Vaccination record updated successfully.'";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = $"vaccinations not found with Id '{id}'";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error retrieving vaccinations: {ex.Message}";
            }
            return response;
        }

    }

}
