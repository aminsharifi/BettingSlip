using BettingSlip.Application.Abstractions.Persistence;
using BettingSlip.Application.BettingSlips.Commands;
using BettingSlip.Application.BettingSlips.DTOs;
using BettingSlip.Application.BettingSlips.Messaging;
using BettingSlip.Core.SlipAggregate;
using System.Data;

namespace BettingSlip.Application.BettingSlips.Services;

public class BettingSlipService(
        IBettingSlipRepository repository,
        IBetPublisher betPublisher,
        IUnitOfWork unitOfWork)
{

    public async Task<Guid> CreateAsync(CreateBettingSlipCommand command)
    {
        var slip = new Slip(command.StakeAmount);
        await repository.AddAsync(slip);

        // Publish via interface
        await betPublisher.PublishBetPlaced(slip.Id, Guid.NewGuid(), slip.StakeAmount);

        return slip.Id;
    }

    public async Task AddSelectionAsync(AddSelectionCommand command)
    {
        await unitOfWork.BeginAsync();
        try
        {
            await repository.SelectionAsync(command);
            await unitOfWork.CommitAsync();
        }
        catch (DBConcurrencyException)
        {
            await unitOfWork.RollbackAsync();
            throw new InvalidOperationException("The betting slip was modified by another process. Please try again.");
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task SubmitAsync(SubmitBettingSlipCommand command)
    {
        await repository.SubmitAsync(command);
    }

    public async Task<List<BettingSlipDto>> ResultAsync()
    {
        return [.. (await repository.ListAsync()).Select(x => x.ToBettingSlipDto())];
    }

    public async Task<BettingSlipDto?> GetAsync(Guid id)
    {
        var slip = await repository.GetByIdAsync(id);
        if (slip is null)
            return null;

        return slip.ToBettingSlipDto();
    }
}