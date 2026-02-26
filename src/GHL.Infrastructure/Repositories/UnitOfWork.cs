using GHL.Domain.Interfaces;
using GHL.Infrastructure.Data;

namespace GHL.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GHLDbContext _context;
    private IWorkSessionRepository? _workSessions;
    
    public UnitOfWork(GHLDbContext context)
    {
        _context = context;
    }
    
    public IWorkSessionRepository WorkSessions => _workSessions ??= new WorkSessionRepository(_context);
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
