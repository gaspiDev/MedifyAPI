using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Auth0Id", "CreatedAt", "Email", "IsActive", "Role", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "auth0|sysadmin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sysadmin@medify.local", true, 0, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "auth0|doctor1", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "dr.john@medify.local", true, 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "auth0|patient1", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.patiente@medify.local", true, 2, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "auth0|patient2", new DateTime(2024, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.patiente@medify.local", true, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Adress", "Dni", "FirstName", "LastName", "LicenseNumber", "Specialty", "UserId" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "123 Medical St", 12345678, "John", "Doe", "LIC-12345", "Cardiology", new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DateOfBirth", "Dni", "FirstName", "LastName", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "456 Main Ave", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 87654321, "María", "Gonzalez", "+541112345678", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "789 Side Rd", new DateTime(1985, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 11223344, "Carlos", "Perez", "+541198765432", new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "CreatedAt", "Diagnosis", "DoctorId", "PatientId", "Reason" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2024, 6, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Follow-up" });

            migrationBuilder.InsertData(
                table: "DoctorPatients",
                columns: new[] { "Id", "AssignedAt", "DoctorId", "IsActive", "Method", "PatientId", "UnassignedAt" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), true, 2, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Appointments",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "DoctorPatients",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));
        }
    }
}
