using GHL.Application.DTOs;

namespace GHL.Application.Interfaces;

/// <summary>
/// Servicio de aplicación para gestión de sesiones de trabajo.
/// </summary>
public interface IWorkSessionService
{
    Task<CurrentSessionDto> GetCurrentSessionAsync(string userId, CancellationToken cancellationToken = default);
    Task<WorkSessionDto> ClockInAsync(string userId, CancellationToken cancellationToken = default);
    Task<WorkSessionDto> ClockOutAsync(string userId, CancellationToken cancellationToken = default);
    Task<BreakDto> StartBreakAsync(string userId, CancellationToken cancellationToken = default);
    Task EndBreakAsync(string userId, CancellationToken cancellationToken = default);
    Task<SessionHistoryResponseDto> GetHistoryAsync(string userId, SessionHistoryRequestDto request, CancellationToken cancellationToken = default);
    Task<DashboardStatsDto> GetDashboardStatsAsync(string userId, CancellationToken cancellationToken = default);
    Task<WeeklyReportDto> GetWeeklyReportAsync(string userId, DateTime? weekStartDate = null, CancellationToken cancellationToken = default);
}
