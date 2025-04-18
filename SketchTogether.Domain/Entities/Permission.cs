using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class Permission : BaseEntity
{
    [Required]
    public Guid DrawingId { get; set; }
        
    public Guid? UserId { get; set; }
        
    [Required]
    public string PermissionType { get; set; } // "VIEW", "EDIT"
        
    public string ShareToken { get; set; }
        
    public DateTime? ExpiresAt { get; set; }
        
    public bool IsActive { get; set; } = true;
        
    // Navigation properties
    [ForeignKey("DrawingId")]
    public virtual Drawing Drawing { get; set; }
        
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}