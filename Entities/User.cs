using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API_VehiclesAPP.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; } = null!;

    public string? Username { get; set; }

    public string PasswordHash { get; set; } = null!;
    
    public string Role { get; set; } = "User";

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

}
