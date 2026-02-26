using GHL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHL.Infrastructure.Data.Configurations;

public class BreakConfiguration : IEntityTypeConfiguration<Break>
{
    public void Configure(EntityTypeBuilder<Break> builder)
    {
        builder.ToTable("breaks");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName("id");
        
        builder.Property(b => b.SessionId)
            .HasColumnName("session_id")
            .IsRequired();
        
        builder.Property(b => b.StartTime)
            .HasColumnName("start_time")
            .IsRequired();
        
        builder.Property(b => b.EndTime)
            .HasColumnName("end_time");
        
        builder.Property(b => b.DurationMinutes)
            .HasColumnName("duration_minutes");
        
        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(b => b.SessionId);
        
        builder.Ignore(b => b.IsActive);
    }
}
