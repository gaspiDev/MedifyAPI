using System;
using Core.Domain.Enums;
using Core.Domain.Interfaces;

namespace Core.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Auth0Id { get; set; }
    public Role Role { get; set; } = Role.User;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
