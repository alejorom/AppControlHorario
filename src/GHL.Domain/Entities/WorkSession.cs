namespace GHL.Domain.Entities;

/// <summary>
/// Sesión de trabajo que representa una jornada laboral.
/// </summary>
public class WorkSession
{
    public Guid Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public DateTime ClockIn { get; private set; }
    public DateTime? ClockOut { get; private set; }
    public int? TotalWorkedMinutes { get; private set; }
    public int? TotalBreakMinutes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    public ApplicationUser? User { get; private set; }
    public ICollection<Break> Breaks { get; private set; } = new List<Break>();
    
    public bool IsActive => ClockOut is null;
    public bool IsOnBreak => Breaks.Any(b => b.IsActive);
    
    private WorkSession() { } // EF Core constructor
    
    public static WorkSession Create(string userId)
    {
        return new WorkSession
        {
            UserId = userId,
            ClockIn = DateTime.UtcNow
        };
    }
    
    public void ClockOutSession()
    {
        if (!IsActive)
            throw new InvalidOperationException("La sesión ya fue cerrada.");
            
        if (IsOnBreak)
            throw new InvalidOperationException("Debe finalizar la pausa antes de marcar salida.");
        
        ClockOut = DateTime.UtcNow;
        CalculateTotals();
        UpdatedAt = DateTime.UtcNow;
    }
    
    public Break StartBreak()
    {
        if (!IsActive)
            throw new InvalidOperationException("No puede iniciar pausa en una sesión cerrada.");
            
        if (IsOnBreak)
            throw new InvalidOperationException("Ya existe una pausa activa.");
        
        var newBreak = Break.Create(Id);
        Breaks.Add(newBreak);
        UpdatedAt = DateTime.UtcNow;
        return newBreak;
    }
    
    public void EndCurrentBreak()
    {
        var activeBreak = Breaks.FirstOrDefault(b => b.IsActive);
        if (activeBreak is null)
            throw new InvalidOperationException("No hay pausa activa para finalizar.");
        
        activeBreak.End();
        UpdatedAt = DateTime.UtcNow;
    }
    
    private void CalculateTotals()
    {
        if (ClockOut is null) return;
        
        var totalMinutes = (int)(ClockOut.Value - ClockIn).TotalMinutes;
        TotalBreakMinutes = Breaks.Sum(b => b.DurationMinutes ?? 0);
        TotalWorkedMinutes = totalMinutes - TotalBreakMinutes;
    }
    
    public int GetCurrentWorkedMinutes()
    {
        var endTime = ClockOut ?? DateTime.UtcNow;
        var totalMinutes = (int)(endTime - ClockIn).TotalMinutes;
        var breakMinutes = Breaks.Sum(b => b.GetDurationMinutes());
        return totalMinutes - breakMinutes;
    }
    
    public int GetCurrentBreakMinutes()
    {
        return Breaks.Sum(b => b.GetDurationMinutes());
    }
}
