using System.ComponentModel.DataAnnotations;

namespace PatientService.Domain.DTOs
{
    public class PatientDTO
    {
        public string Name { get; set; } = null!;

        
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;


       
        public string PhoneNumber { get; set; } = null!;
    }
}
