using GHL.Domain.Entities;
using GHL.Domain.Interfaces;
using GHL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GHL.Infrastructure.Repositories;

public class WorkSessionRepository : IWorkSessionRepository
{
    private readonly GHLDbContext _context;
    
    public WorkSessionRepository(GHLDbContext context)
    {
        _context = context;
    }
    
    public async Task<WorkSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(w => w.Breaks)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }
    
    public async Task<WorkSession?> GetActiveSessionByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.WorkSessions
            .Include(w => w.Breaks)
            .FirstOrDefaultAsync(w => w.UserId == userId && w.ClockOut == null, cancellationToken);
    }
    
    public async Task<WorkSession> AddAsync(WorkSession session, CancellationToken cancellationToken = default)
    {
        await _context.WorkSessions.AddAsync(session, cancellationToken);
        return session;
    }
    
    public Task UpdateAsync(WorkSession session, CancellationToken cancellationToken = default)
    {
        // Entity Framework Core already tracks the entity since it was retrieved using GetByIdAsync or GetActiveSessionByUserIdAsync
        // Calling .Update(session) forces all properties to be marked as Modified, which can cause ConcurrencyExceptions 
        // with properties like CreatedAt or UpdatedAt that have DB Default Values.
        return Task.CompletedTask;
    }
    
    public async Task<IEnumerable<WorkSession>> GetSessionsByUserIdAsync(
        string userId, 
        DateTime? startDate = null, 
        DateTime? endDate = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var query = _context.WorkSessions
            .Include(w => w.Breaks)
            .Where(w => w.UserId == userId);
        
        if (startDate.HasValue)
            query = query.Where(w => w.ClockIn >= startDate.Value);
        
        if (endDate.HasValue)
            query = query.Where(w => w.ClockIn <= endDate.Value);
        
        return await query
            .OrderByDescending(w => w.ClockIn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<int> GetTotalSessionsCountAsync(
        string userId, 
        DateTime? startDate = null, 
        DateTime? endDate = null, 
        CancellationToken cancellationToken = default)
    {
        var query = _context.WorkSessions.Where(w => w.UserId == userId);
        
        if (startDate.HasValue)
            query = query.Where(w => w.ClockIn >= startDate.Value);
        
        if (endDate.HasValue)
            query = query.Where(w => w.ClockIn <= endDate.Value);
        
        return await query.CountAsync(cancellationToken);
    }
}
