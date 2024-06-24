using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientService.Domain.Entities
{

    [Table("tbl_patient")]
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string Name { get; set; } = null!;

  
        public DateTime DateOfBirth { get; set; }

       
        public string Email { get; set; } = null!;

       
        public string PhoneNumber { get; set; } = null!;

        public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();

        public string VaccinationStatus { get; set; } = null!;

    }
}
