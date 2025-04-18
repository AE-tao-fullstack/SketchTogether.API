using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class DrawingAction : BaseEntity
{
    [Required]
    public Guid DrawingId { get; set; }
        
    [Required]
    public Guid UserId { get; set; }
        
    [Required]
    public string ActionType { get; set; }
        
    [Column(TypeName = "jsonb")]
    public string ActionData { get; set; }
        
    public Guid? BatchId { get; set; }
        
    public bool IsUndone { get; set; } = false;
        
    // Navigation properties
    [ForeignKey("DrawingId")]
    public virtual Drawing Drawing { get; set; }
        
    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}