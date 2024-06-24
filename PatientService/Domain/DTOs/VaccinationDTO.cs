using System.ComponentModel.DataAnnotations;

namespace PatientService.Domain.DTOs
{
    public class VaccinationDTO
    {
        public int VaccinationId { get; set; }

        public string VaccineName { get; set; } = null!;

    
        public DateTime VaccinationDate { get; set; }

        
        public int DoseNumber { get; set; }
        public int PatientId { get; set; }
    }
}
