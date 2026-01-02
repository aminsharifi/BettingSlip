namespace BettingSlip.Infrastructure.Persistence;

using BettingSlip.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

public class EfUnitOfWork(BettingSlipDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task BeginAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction is null) return;
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is null) return;
        await _transaction.RollbackAsync();
    }
}
