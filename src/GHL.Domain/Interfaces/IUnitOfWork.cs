namespace GHL.Domain.Interfaces;

/// <summary>
/// Unit of Work para manejar transacciones.
/// </summary>
public interface IUnitOfWork
{
    IWorkSessionRepository WorkSessions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
