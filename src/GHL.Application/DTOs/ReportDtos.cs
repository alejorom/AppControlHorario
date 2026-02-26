namespace GHL.Application.DTOs;

public record DashboardStatsDto
{
    public int TodayWorkedMinutes { get; init; }
    public int TodayBreakMinutes { get; init; }
    public int WeekWorkedMinutes { get; init; }
    public int MonthWorkedMinutes { get; init; }
    public int DaysWorkedThisMonth { get; init; }
    public double AverageDailyMinutes { get; init; }
    
    public string FormattedTodayWorked => FormatMinutes(TodayWorkedMinutes);
    public string FormattedTodayBreak => FormatMinutes(TodayBreakMinutes);
    public string FormattedWeekWorked => FormatMinutes(WeekWorkedMinutes);
    public string FormattedMonthWorked => FormatMinutes(MonthWorkedMinutes);
    public string FormattedAverageDaily => FormatMinutes((int)AverageDailyMinutes);
    
    private static string FormatMinutes(int minutes)
    {
        var hours = minutes / 60;
        var mins = minutes % 60;
        return $"{hours}h {mins}m";
    }
}

public record WeeklyReportDto
{
    public DateTime WeekStartDate { get; init; }
    public List<DailyWorkDto> DailyData { get; init; } = new();
    public int TotalMinutes { get; init; }
    public string FormattedTotal => FormatMinutes(TotalMinutes);
    
    private static string FormatMinutes(int minutes)
    {
        var hours = minutes / 60;
        var mins = minutes % 60;
        return $"{hours}h {mins}m";
    }
}

public record DailyWorkDto
{
    public DateTime Date { get; init; }
    public int WorkedMinutes { get; init; }
    public string DayName { get; init; } = string.Empty;
    public double Hours => Math.Round(WorkedMinutes / 60.0, 1);
}
