using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientService.Domain.Entities
{

    [Table("tbl_vaccination")]
    public class Vaccination
    {
        [Key]
        public int VaccinationId { get; set; }


        public string VaccineName { get; set; } = null!;

        public DateTime VaccinationDate { get; set; }

        public int DoseNumber { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
