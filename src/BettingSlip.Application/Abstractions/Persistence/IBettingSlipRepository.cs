using BettingSlip.Application.BettingSlips.Commands;
using BettingSlip.Core.SlipAggregate;

namespace BettingSlip.Application.Abstractions.Persistence;
public interface IBettingSlipRepository
{
    Task<Slip?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Slip?>> ListAsync();
    Task AddAsync(Slip bettingSlip);
    Task SelectionAsync(AddSelectionCommand command);

    Task SubmitAsync(SubmitBettingSlipCommand command);
}

