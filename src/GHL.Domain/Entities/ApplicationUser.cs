using Microsoft.AspNetCore.Identity;

namespace GHL.Domain.Entities;

/// <summary>
/// Usuario de la aplicación con campos extendidos de Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
    
    public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();
}
