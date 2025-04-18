using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class DrawingSession : BaseEntity
{
    [Required]
    public Guid DrawingId { get; set; }
        
    public bool IsActive { get; set; } = true;
        
    // Navigation properties
    [ForeignKey("DrawingId")]
    public virtual Drawing Drawing { get; set; }
        
    public virtual ICollection<UserSession> UserSessions { get; set; }
}