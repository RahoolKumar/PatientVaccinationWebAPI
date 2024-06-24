using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;
using PatientService.Repositories.Interfaces;
using PatientService.Validators;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet("GetAllPatientsHistory")]
        public async Task<IActionResult> GetAllPatientsHistory()
        {

            var patients = await _patientRepository.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("GetPatientById/{Id}")]
        public async Task<IActionResult> GetPatientById(int Id)
        {
            return Ok(await _patientRepository.GetPatientById(Id));
            
           
        }
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient(PatientDTO patientDto)
        {
           
            return Ok(await _patientRepository.AddPatient(patientDto));

        }
        [HttpDelete("DeletePatientById/{id}")]
        public async Task<IActionResult> DeletePatientById(int id)
        {
            await _patientRepository.DeletePatient(id);
            return NoContent();
        }
        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(PatientDTO patient,int id)
        {
            await _patientRepository.UpdatePatient(patient,id);
            return NoContent();
        }
        [HttpGet("CheckPatientVaccinationStatus/{Id}")]
        public async Task<IActionResult> CheckPatientVaccinationStatus(int Id)
        {
            return Ok(await _patientRepository.ShowVaccinationStatus(Id));


        }

    }
}