using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SketchTogether.Domain.Entities;

public class Drawing : BaseEntity
{
    [Required]
    public string Title { get; set; }
        
    public string Description { get; set; }
        
    [Required]
    public Guid OwnerId { get; set; }
        
    public int Width { get; set; } = 1024;
        
    public int Height { get; set; } = 768;
        
    public string ThumbnailUrl { get; set; }
        
    public bool IsPublic { get; set; } = false;
        
    public string[] Tags { get; set; }
        
    public string Category { get; set; }
        
    [Column(TypeName = "jsonb")]
    public string BackgroundLayerData { get; set; } = "{}";
        
    [Column(TypeName = "jsonb")]
    public string DrawingLayerData { get; set; } = "{}";
        
    // Navigation properties
    [ForeignKey("OwnerId")]
    public virtual User Owner { get; set; }
        
    public virtual ICollection<DrawingSession> DrawingSessions { get; set; }
    public virtual ICollection<DrawingAction> DrawingActions { get; set; }
    public virtual ICollection<DrawingVersion> DrawingVersions { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
}