using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Application.BettingSlips.Commands;
using BettingSlip.Core.SlipAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BettingSlip.Infrastructure.Persistence.Repositories;

public class BettingSlipRepository(BettingSlipDbContext context) : IBettingSlipRepository
{

    public async Task AddAsync(Slip slip)
    {
        await context.Slips.AddAsync(slip);
        await context.SaveChangesAsync();
    }

    public async Task<Slip?> GetByIdAsync(Guid id)
    {
        var _slip = await context.Slips
           .Where(x => x.Id == id)
           .Include(x => x.Selections)
           .FirstOrDefaultAsync();

        return _slip;
    }

    public async Task SelectionAsync(AddSelectionCommand command)
    {
        Slip slip = await GetByIdAsync(command.SlipId)
               ?? throw new InvalidOperationException("Betting slip not found.");

        slip.AddSelection(slip.Id, command.EventName, command.Market, command.Odd);

        await context.Selections.AddAsync(new Selection(slip.Id, command.EventName, command.Market, command.Odd));

        context.Attach(slip);

        context.Entry(slip).State = EntityState.Modified;

        await context.SaveChangesAsync();
    }

    public async Task SubmitAsync(SubmitBettingSlipCommand command)
    {
        var slip = await GetByIdAsync(command.SlipId)
            ?? throw new InvalidOperationException("Betting slip not found.");

        slip.Submit();

        context.Update(slip);

        await context.SaveChangesAsync();
    }
}
