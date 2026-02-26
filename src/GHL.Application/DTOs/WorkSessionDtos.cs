namespace GHL.Application.DTOs;

public record WorkSessionDto
{
    public Guid Id { get; init; }
    public string UserId { get; init; } = string.Empty;
    public DateTime ClockIn { get; init; }
    public DateTime? ClockOut { get; init; }
    
    public DateTime LocalClockIn => DateTime.SpecifyKind(ClockIn, DateTimeKind.Utc).ToLocalTime();
    public DateTime? LocalClockOut => ClockOut.HasValue ? DateTime.SpecifyKind(ClockOut.Value, DateTimeKind.Utc).ToLocalTime() : null;
    public int? TotalWorkedMinutes { get; init; }
    public int? TotalBreakMinutes { get; init; }
    public bool IsActive { get; init; }
    public bool IsOnBreak { get; init; }
    public List<BreakDto> Breaks { get; init; } = new();
    
    public string FormattedWorkedTime => FormatMinutes(TotalWorkedMinutes ?? 0);
    public string FormattedBreakTime => FormatMinutes(TotalBreakMinutes ?? 0);
    
    private static string FormatMinutes(int minutes)
    {
        var hours = minutes / 60;
        var mins = minutes % 60;
        return $"{hours}h {mins}m";
    }
}

public record BreakDto
{
    public Guid Id { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
    public int? DurationMinutes { get; init; }
    public bool IsActive { get; init; }
}

public record CurrentSessionDto
{
    public WorkSessionDto? Session { get; init; }
    public int CurrentWorkedMinutes { get; init; }
    public int CurrentBreakMinutes { get; init; }
    public string Status { get; init; } = "Fuera de jornada";
    
    public string FormattedWorkedTime
    {
        get
        {
            var hours = CurrentWorkedMinutes / 60;
            var mins = CurrentWorkedMinutes % 60;
            return $"{hours}h {mins}m";
        }
    }
    
    public string FormattedBreakTime
    {
        get
        {
            var hours = CurrentBreakMinutes / 60;
            var mins = CurrentBreakMinutes % 60;
            return $"{hours}h {mins}m";
        }
    }
}

public record SessionHistoryRequestDto
{
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public record SessionHistoryResponseDto
{
    public List<WorkSessionDto> Sessions { get; init; } = new();
    public int TotalCount { get; init; }
    public int TotalPages { get; init; }
    public int CurrentPage { get; init; }
}
