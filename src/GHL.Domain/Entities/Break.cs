namespace GHL.Domain.Entities;

/// <summary>
/// Pausa durante una jornada laboral.
/// </summary>
public class Break
{
    public Guid Id { get; private set; }
    public Guid SessionId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public int? DurationMinutes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    public WorkSession? Session { get; private set; }
    
    public bool IsActive => EndTime is null;
    
    private Break() { } // EF Core constructor
    
    public static Break Create(Guid sessionId)
    {
        return new Break
        {
            SessionId = sessionId,
            StartTime = DateTime.UtcNow
        };
    }
    
    public void End()
    {
        if (!IsActive)
            throw new InvalidOperationException("La pausa ya fue finalizada.");
        
        EndTime = DateTime.UtcNow;
        DurationMinutes = (int)(EndTime.Value - StartTime).TotalMinutes;
    }
    
    public int GetDurationMinutes()
    {
        if (DurationMinutes.HasValue) return DurationMinutes.Value;
        
        var endTime = EndTime ?? DateTime.UtcNow;
        return (int)(endTime - StartTime).TotalMinutes;
    }
}
