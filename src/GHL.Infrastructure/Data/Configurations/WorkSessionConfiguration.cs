using GHL.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GHL.Infrastructure.Data.Configurations;

public class WorkSessionConfiguration : IEntityTypeConfiguration<WorkSession>
{
    public void Configure(EntityTypeBuilder<WorkSession> builder)
    {
        builder.ToTable("work_sessions");
        
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.Id)
            .HasColumnName("id");
        
        builder.Property(w => w.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(w => w.ClockIn)
            .HasColumnName("clock_in")
            .IsRequired();
        
        builder.Property(w => w.ClockOut)
            .HasColumnName("clock_out");
        
        builder.Property(w => w.TotalWorkedMinutes)
            .HasColumnName("total_worked_minutes");
        
        builder.Property(w => w.TotalBreakMinutes)
            .HasColumnName("total_break_minutes");
        
        builder.Property(w => w.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAdd();
        
        builder.Property(w => w.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("GETUTCDATE()")
            .ValueGeneratedOnAddOrUpdate();
        
        builder.HasIndex(w => w.UserId);
        builder.HasIndex(w => w.ClockIn);
        
        builder.HasMany(w => w.Breaks)
            .WithOne(b => b.Session)
            .HasForeignKey(b => b.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Ignore(w => w.IsActive);
        builder.Ignore(w => w.IsOnBreak);
    }
}
