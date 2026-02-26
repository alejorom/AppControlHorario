using GHL.Application.DTOs;
using GHL.Application.Interfaces;
using GHL.Domain.Entities;
using GHL.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GHL.Application.Services;

public class WorkSessionService : IWorkSessionService
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public WorkSessionService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    public async Task<CurrentSessionDto> GetCurrentSessionAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var session = await _unitOfWork.WorkSessions.GetActiveSessionByUserIdAsync(userId, cancellationToken);
        
        if (session is null)
        {
            return new CurrentSessionDto
            {
                Session = null,
                CurrentWorkedMinutes = 0,
                CurrentBreakMinutes = 0,
                Status = "Fuera de jornada"
            };
        }
        
        var status = session.IsOnBreak ? "En pausa" : "Trabajando";
        
        return new CurrentSessionDto
        {
            Session = MapToDto(session),
            CurrentWorkedMinutes = session.GetCurrentWorkedMinutes(),
            CurrentBreakMinutes = session.GetCurrentBreakMinutes(),
            Status = status
        };
    }
    
    public async Task<WorkSessionDto> ClockInAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var existingSession = await _unitOfWork.WorkSessions.GetActiveSessionByUserIdAsync(userId, cancellationToken);
        if (existingSession is not null)
            throw new InvalidOperationException("Ya existe una jornada activa. Debe marcar salida primero.");
        
        var session = WorkSession.Create(userId);
        await _unitOfWork.WorkSessions.AddAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return MapToDto(session);
    }
    
    public async Task<WorkSessionDto> ClockOutAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var session = await _unitOfWork.WorkSessions.GetActiveSessionByUserIdAsync(userId, cancellationToken);
        if (session is null)
            throw new InvalidOperationException("No hay jornada activa para cerrar.");
        
        session.ClockOutSession();
        await _unitOfWork.WorkSessions.UpdateAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return MapToDto(session);
    }
    
    public async Task<BreakDto> StartBreakAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var session = await _unitOfWork.WorkSessions.GetActiveSessionByUserIdAsync(userId, cancellationToken);
        if (session is null)
            throw new InvalidOperationException("No hay jornada activa. Debe marcar entrada primero.");
        
        var breakItem = session.StartBreak();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return MapBreakToDto(breakItem);
    }
    
    public async Task EndBreakAsync(string userId, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var session = await _unitOfWork.WorkSessions.GetActiveSessionByUserIdAsync(userId, cancellationToken);
        if (session is null)
            throw new InvalidOperationException("No hay jornada activa.");
        
        session.EndCurrentBreak();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<SessionHistoryResponseDto> GetHistoryAsync(string userId, SessionHistoryRequestDto request, CancellationToken cancellationToken = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var sessions = await _unitOfWork.WorkSessions.GetSessionsByUserIdAsync(
            userId, 
            request.StartDate, 
            request.EndDate, 
            request.PageNumber, 
            request.PageSize, 
            cancellationToken);
        
        var totalCount = await _unitOfWork.WorkSessions.GetTotalSessionsCountAsync(
            userId, 
            request.StartDate, 
            request.EndDate, 
            cancellationToken);
        
        var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
        
        return new SessionHistoryResponseDto
        {
            Sessions = sessions.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            TotalPages = totalPages,
            CurrentPage = request.PageNumber
        };
    }
    
    public async Task<DashboardStatsDto> GetDashboardStatsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var todayStart = now.Date;
        var weekStart = todayStart.AddDays(-(int)todayStart.DayOfWeek);
        var monthStart = new DateTime(now.Year, now.Month, 1);
        
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var monthSessions = await _unitOfWork.WorkSessions.GetSessionsByUserIdAsync(
            userId, monthStart, now, 1, 100, cancellationToken);
        
        var sessionsList = monthSessions.ToList();
        
        var todayMinutes = sessionsList
            .Where(s => s.ClockIn.Date == todayStart)
            .Sum(s => s.TotalWorkedMinutes ?? s.GetCurrentWorkedMinutes());
            
        var todayBreakMinutes = sessionsList
            .Where(s => s.ClockIn.Date == todayStart)
            .Sum(s => s.TotalBreakMinutes ?? s.GetCurrentBreakMinutes());
        
        var weekMinutes = sessionsList
            .Where(s => s.ClockIn >= weekStart)
            .Sum(s => s.TotalWorkedMinutes ?? s.GetCurrentWorkedMinutes());
        
        var monthMinutes = sessionsList
            .Sum(s => s.TotalWorkedMinutes ?? s.GetCurrentWorkedMinutes());
        
        var daysWorked = sessionsList.Select(s => s.ClockIn.Date).Distinct().Count();
        var avgDaily = daysWorked > 0 ? (double)monthMinutes / daysWorked : 0;
        
        return new DashboardStatsDto
        {
            TodayWorkedMinutes = todayMinutes,
            TodayBreakMinutes = todayBreakMinutes,
            WeekWorkedMinutes = weekMinutes,
            MonthWorkedMinutes = monthMinutes,
            DaysWorkedThisMonth = daysWorked,
            AverageDailyMinutes = avgDaily
        };
    }
    
    public async Task<WeeklyReportDto> GetWeeklyReportAsync(string userId, DateTime? weekStartDate = null, CancellationToken cancellationToken = default)
    {
        var startDate = weekStartDate ?? DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        var endDate = startDate.AddDays(7);
        
        using var scope = _scopeFactory.CreateScope();
        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var sessions = await _unitOfWork.WorkSessions.GetSessionsByUserIdAsync(
            userId, startDate, endDate, 1, 100, cancellationToken);
        
        var dailyData = Enumerable.Range(0, 7)
            .Select(i => startDate.AddDays(i))
            .Select(date => new DailyWorkDto
            {
                Date = date,
                DayName = date.ToString("ddd"),
                WorkedMinutes = sessions
                    .Where(s => s.ClockIn.Date == date)
                    .Sum(s => s.TotalWorkedMinutes ?? s.GetCurrentWorkedMinutes())
            })
            .ToList();
        
        return new WeeklyReportDto
        {
            WeekStartDate = startDate,
            DailyData = dailyData,
            TotalMinutes = dailyData.Sum(d => d.WorkedMinutes)
        };
    }
    
    private static WorkSessionDto MapToDto(WorkSession session)
    {
        return new WorkSessionDto
        {
            Id = session.Id,
            UserId = session.UserId,
            ClockIn = session.ClockIn,
            ClockOut = session.ClockOut,
            TotalWorkedMinutes = session.TotalWorkedMinutes ?? session.GetCurrentWorkedMinutes(),
            TotalBreakMinutes = session.TotalBreakMinutes ?? session.GetCurrentBreakMinutes(),
            IsActive = session.IsActive,
            IsOnBreak = session.IsOnBreak,
            Breaks = session.Breaks.Select(MapBreakToDto).ToList()
        };
    }
    
    private static BreakDto MapBreakToDto(Break breakItem)
    {
        return new BreakDto
        {
            Id = breakItem.Id,
            StartTime = breakItem.StartTime,
            EndTime = breakItem.EndTime,
            DurationMinutes = breakItem.DurationMinutes,
            IsActive = breakItem.IsActive
        };
    }
}
