using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class UserSession : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    public Guid SessionId { get; set; }
        
    [Required]
    public string ConnectionId { get; set; }
        
    public bool IsActive { get; set; } = true;
        
    [Column(TypeName = "jsonb")]
    public string CursorPosition { get; set; } = "{\"x\": 0, \"y\": 0}";
        
    public DateTime LastActivity { get; set; }
        
    // Navigation properties
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
        
    [ForeignKey("SessionId")]
    public virtual DrawingSession Session { get; set; }
}