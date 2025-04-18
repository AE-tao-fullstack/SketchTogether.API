using Microsoft.EntityFrameworkCore;
using SketchTogether.Domain.Entities;

namespace SketchTogether.Domain;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
        
    public DbSet<User> Users { get; set; }
    public DbSet<Drawing> Drawings { get; set; }
    public DbSet<DrawingSession> DrawingSessions { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<DrawingAction> DrawingActions { get; set; }
    public DbSet<DrawingVersion> DrawingVersions { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
                
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
                
            // Configure Drawing entity
            modelBuilder.Entity<Drawing>()
                .HasIndex(d => d.OwnerId);
                
            modelBuilder.Entity<Drawing>()
                .HasIndex(d => d.Title);
                
            modelBuilder.Entity<Drawing>()
                .HasIndex(d => d.Tags)
                .HasMethod("GIN");
                
            // Configure DrawingSession entity
            modelBuilder.Entity<DrawingSession>()
                .HasIndex(ds => new { ds.DrawingId, ds.IsActive });
                
            // Configure UserSession entity
            modelBuilder.Entity<UserSession>()
                .HasIndex(us => new { us.SessionId, us.UserId });
                
            modelBuilder.Entity<UserSession>()
                .HasIndex(us => us.ConnectionId);
                
            // Configure DrawingAction entity
            modelBuilder.Entity<DrawingAction>()
                .HasIndex(da => da.DrawingId);
                
            modelBuilder.Entity<DrawingAction>()
                .HasIndex(da => da.BatchId);
                
            // Configure DrawingVersion entity
            modelBuilder.Entity<DrawingVersion>()
                .HasIndex(dv => dv.DrawingId);
                
            // Configure Permission entity
            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.DrawingId, p.UserId });
                
            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.ShareToken)
                .IsUnique()
                .HasFilter("\"ShareToken\" IS NOT NULL");
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }
        
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }
        
        private void UpdateTimestamps()
        {
            var now = DateTime.UtcNow;
            
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.Id = entry.Entity.Id == Guid.Empty ? Guid.NewGuid() : entry.Entity.Id;
                    entry.Entity.CreatedAt = now;
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    
                    // Don't modify the CreatedAt during updates
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                }
            }
        }
}