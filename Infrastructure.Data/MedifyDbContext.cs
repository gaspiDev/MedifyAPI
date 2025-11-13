using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Enums;

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
            modelBuilder.Entity<DoctorPatient>()
                .HasIndex(dp => new { dp.DoctorId, dp.PatientId })
                .IsUnique();

            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Seed data: Users, Doctors, Patients, DoctorPatients, Appointments
            // Static GUIDs so EF Core can track relationships in HasData
            var sysadminUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var doctorUserId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var patientUser1Id = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var patientUser2Id = Guid.Parse("44444444-4444-4444-4444-444444444444");

            var doctorId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var patient1Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var patient2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");

            var doctorPatient1Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var appointment1Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = sysadminUserId,
                    Email = "sysadmin@medify.local",
                    Auth0Id = "auth0|sysadmin",
                    Role = Role.Sysadmin,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 1, 1)
                },
                new User
                {
                    Id = doctorUserId,
                    Email = "dr.john@medify.local",
                    Auth0Id = "auth0|doctor1",
                    Role = Role.User,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 2, 1)
                },
                new User
                {
                    Id = patientUser1Id,
                    Email = "maria.patiente@medify.local",
                    Auth0Id = "auth0|patient1",
                    Role = Role.User,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 3, 1)
                },
                new User
                {
                    Id = patientUser2Id,
                    Email = "carlos.patiente@medify.local",
                    Auth0Id = "auth0|patient2",
                    Role = Role.User,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 3, 2)
                }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = doctorId,
                    FirstName = "John",
                    LastName = "Doe",
                    Specialty = "Cardiology",
                    LicenseNumber = "LIC-12345",
                    Adress = "123 Medical St",
                    Dni = 12345678,
                    UserId = doctorUserId
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = patient1Id,
                    FirstName = "María",
                    LastName = "Gonzalez",
                    Dni = 87654321,
                    Address = "456 Main Ave",
                    PhoneNumber = "+541112345678",
                    DateOfBirth = new DateTime(1990, 5, 15),
                    UserId = patientUser1Id
                },
                new Patient
                {
                    Id = patient2Id,
                    FirstName = "Carlos",
                    LastName = "Perez",
                    Dni = 11223344,
                    Address = "789 Side Rd",
                    PhoneNumber = "+541198765432",
                    DateOfBirth = new DateTime(1985, 8, 20),
                    UserId = patientUser2Id
                }
            );

            modelBuilder.Entity<DoctorPatient>().HasData(
                new DoctorPatient
                {
                    Id = doctorPatient1Id,
                    AssignedAt = new DateTime(2024, 6, 1),
                    Method = AssociationMethod.Manual,
                    IsActive = true,
                    UnassignedAt = null,
                    DoctorId = doctorId,
                    PatientId = patient1Id
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = appointment1Id,
                    CreatedAt = new DateTime(2024, 6, 10),
                    AppointmentDate = new DateTime(2024, 6, 20, 14, 30, 0),
                    Reason = "Follow-up",
                    Diagnosis = null,
                    DoctorId = doctorId,
                    PatientId = patient2Id
                }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
