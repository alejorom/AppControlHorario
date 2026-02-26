using GHL.Domain.Entities;

namespace GHL.Domain.Interfaces;

/// <summary>
/// Repositorio para operaciones de sesiones de trabajo.
/// </summary>
public interface IWorkSessionRepository
{
    Task<WorkSession?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<WorkSession?> GetActiveSessionByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<WorkSession> AddAsync(WorkSession session, CancellationToken cancellationToken = default);
    Task UpdateAsync(WorkSession session, CancellationToken cancellationToken = default);
    Task<IEnumerable<WorkSession>> GetSessionsByUserIdAsync(
        string userId, 
        DateTime? startDate = null, 
        DateTime? endDate = null,
        int pageNumber = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
    Task<int> GetTotalSessionsCountAsync(string userId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
}
