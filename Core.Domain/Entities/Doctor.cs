using Core.Domain.Interfaces;
using System;

namespace Core.Domain.Entities;

public class Doctor : IEntity
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Specialty { get; set; }
    public required string LicenseNumber { get; set; }
    public required string Adress { get; set; }
    public required int Dni{ get; set; }

    public required Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
