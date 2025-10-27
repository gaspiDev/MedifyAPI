using System;

namespace Core.Domain.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required int Dni { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public required Guid UserId { get; set; }
    public User User { get; set; } = null!;

}
