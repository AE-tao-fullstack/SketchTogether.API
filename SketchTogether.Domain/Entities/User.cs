using System.ComponentModel.DataAnnotations;

namespace SketchTogether.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
        
    [Required]
    public string Username { get; set; }
        
    [Required]
    public string PasswordHash { get; set; }
        
    public DateTime? LastLoginAt { get; set; }
        
    // Navigation properties
    public virtual ICollection<Drawing> Drawings { get; set; }
    public virtual ICollection<UserSession> UserSessions { get; set; }
    public virtual ICollection<DrawingAction> DrawingActions { get; set; }
    public virtual ICollection<DrawingVersion> DrawingVersions { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
}