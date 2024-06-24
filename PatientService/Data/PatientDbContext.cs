using Microsoft.EntityFrameworkCore;
using PatientService.Domain.Entities;

namespace PatientService.Data
{
    public class PatientDbContext: DbContext
    {
        public PatientDbContext()
        {
            
        }
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }
     
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Vaccinations)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
