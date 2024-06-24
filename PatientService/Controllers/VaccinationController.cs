using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientService.Domain.DTOs;
using PatientService.Domain.Entities;
using PatientService.Repositories.Implementations;
using PatientService.Repositories.Interfaces;

namespace PatientService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : ControllerBase
    {
        private readonly IVaccinationRepository _vaccinationRepository;

        public VaccinationController(IVaccinationRepository vaccinationRepository)
        {
            _vaccinationRepository = vaccinationRepository;
        }
        [HttpPost("PatientsVaccinationEntry")]
        public async Task<IActionResult> PatientsVaccinationEntry(VaccinationDTO vaccinationDTO)
        {

            return Ok(await _vaccinationRepository.AddVaccination(vaccinationDTO));

        }
        [HttpGet("PatientHistory/{patientId}")]
        public async Task<IActionResult> PatientHistory(int patientId)
        {
            var vaccinations = await _vaccinationRepository.GetVaccinationsByPatientId(patientId);
            return Ok(vaccinations);
        }

        [HttpGet("GetVaccinationById/{vaccinationId}")]
        public async Task<IActionResult> GetVaccinationById(int vaccinationId)
        {
            var vaccinations = await _vaccinationRepository.GetVaccinationById(vaccinationId);
            return Ok(vaccinations);
        }
        [HttpPut("UpdateVaccinationRecord/{id}")]
        public async Task<IActionResult> UpdatePatient(VaccinationDTO vaccinationDTO, int id)
        {
            await _vaccinationRepository.UpdateVacination(vaccinationDTO, id);
            return NoContent();
        }
    }
}
