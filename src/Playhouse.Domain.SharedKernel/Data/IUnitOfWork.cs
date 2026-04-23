namespace Playhouse.Domain.SharedKernel.Data;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync(CancellationToken cancellation = default);

    Task CommitTransactionAsync(CancellationToken cancellation = default);

    Task RollbackTransactionAsync(CancellationToken cancellation = default);

    bool HasActiveTransaction();
}
