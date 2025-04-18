using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class DrawingVersion : BaseEntity
{
    [Required]
    public Guid DrawingId { get; set; }
        
    [Required]
    public Guid CreatedById { get; set; }
        
    [Required]
    public string VersionName { get; set; }
        
    [Column(TypeName = "jsonb")]
    public string BackgroundLayerData { get; set; }
        
    [Column(TypeName = "jsonb")]
    public string DrawingLayerData { get; set; }
        
    // Navigation properties
    [ForeignKey("DrawingId")]
    public virtual Drawing Drawing { get; set; }
        
    [ForeignKey("CreatedById")]
    public virtual User CreatedBy { get; set; }
}