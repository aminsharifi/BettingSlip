namespace BettingSlip.Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task BeginAsync();
    Task CommitAsync();
    Task RollbackAsync();
}

