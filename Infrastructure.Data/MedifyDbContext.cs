using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MedifyDbContext : DbContext
    {
        public MedifyDbContext(DbContextOptions<MedifyDbContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<DoctorPatient> DoctorPatients { get; set; } = null!;
        public DbSet<AssociationInvite> AssociationInvites { get; set; } = null!;
        public DbSet<Study> Studies { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación única Doctor-Paciente
            modelBuilder.Entity<DoctorPatient>()
                .HasIndex(dp => new { dp.DoctorId, dp.PatientId })
                .IsUnique();

            // 🔹 Desactivar borrado en cascada en relaciones que apuntan a Patient o Doctor
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
