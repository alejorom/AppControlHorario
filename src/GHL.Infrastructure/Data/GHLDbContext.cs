using GHL.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GHL.Infrastructure.Data;

/// <summary>
/// Contexto de base de datos para el sistema de gestión de horarios con Identity.
/// </summary>
public class GHLDbContext : IdentityDbContext<ApplicationUser>
{
    public GHLDbContext(DbContextOptions<GHLDbContext> options) : base(options) { }
    
    public DbSet<WorkSession> WorkSessions => Set<WorkSession>();
    public DbSet<Break> Breaks => Set<Break>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GHLDbContext).Assembly);
        
        // Configure ApplicationUser
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FullName).HasMaxLength(200);
            entity.Property(u => u.AvatarUrl).HasMaxLength(500);
        });
        
        // Configure WorkSession relationship
        modelBuilder.Entity<WorkSession>(entity =>
        {
            entity.HasOne(w => w.User)
                  .WithMany(u => u.WorkSessions)
                  .HasForeignKey(w => w.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
