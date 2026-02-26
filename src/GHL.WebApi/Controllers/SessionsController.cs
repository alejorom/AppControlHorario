using GHL.Application.DTOs;
using GHL.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GHL.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController : ControllerBase
{
    private readonly IWorkSessionService _workSessionService;
    
    public SessionsController(IWorkSessionService workSessionService)
    {
        _workSessionService = workSessionService;
    }
    
    /// <summary>
    /// Obtiene la sesión de trabajo activa del usuario.
    /// </summary>
    [HttpGet("current/{userId}")]
    public async Task<ActionResult<CurrentSessionDto>> GetCurrentSession(string userId, CancellationToken cancellationToken)
    {
        var result = await _workSessionService.GetCurrentSessionAsync(userId, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Marca entrada - inicia una nueva jornada laboral.
    /// </summary>
    [HttpPost("clock-in/{userId}")]
    public async Task<ActionResult<WorkSessionDto>> ClockIn(string userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _workSessionService.ClockInAsync(userId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Marca salida - finaliza la jornada laboral activa.
    /// </summary>
    [HttpPost("clock-out/{userId}")]
    public async Task<ActionResult<WorkSessionDto>> ClockOut(string userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _workSessionService.ClockOutAsync(userId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Inicia una pausa durante la jornada activa.
    /// </summary>
    [HttpPost("break/start/{userId}")]
    public async Task<ActionResult<BreakDto>> StartBreak(string userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _workSessionService.StartBreakAsync(userId, cancellationToken);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Finaliza la pausa activa.
    /// </summary>
    [HttpPost("break/end/{userId}")]
    public async Task<IActionResult> EndBreak(string userId, CancellationToken cancellationToken)
    {
        try
        {
            await _workSessionService.EndBreakAsync(userId, cancellationToken);
            return Ok(new { message = "Pausa finalizada correctamente" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    /// <summary>
    /// Obtiene el historial de jornadas del usuario.
    /// </summary>
    [HttpGet("history/{userId}")]
    public async Task<ActionResult<SessionHistoryResponseDto>> GetHistory(
        string userId, 
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var request = new SessionHistoryRequestDto
        {
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        var result = await _workSessionService.GetHistoryAsync(userId, request, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Obtiene estadísticas del dashboard.
    /// </summary>
    [HttpGet("stats/{userId}")]
    public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats(string userId, CancellationToken cancellationToken)
    {
        var result = await _workSessionService.GetDashboardStatsAsync(userId, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Obtiene el reporte semanal.
    /// </summary>
    [HttpGet("report/weekly/{userId}")]
    public async Task<ActionResult<WeeklyReportDto>> GetWeeklyReport(
        string userId, 
        [FromQuery] DateTime? weekStartDate,
        CancellationToken cancellationToken)
    {
        var result = await _workSessionService.GetWeeklyReportAsync(userId, weekStartDate, cancellationToken);
        return Ok(result);
    }
}
