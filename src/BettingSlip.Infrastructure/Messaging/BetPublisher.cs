using MassTransit;
using BettingSlip.Contracts.Events;
using BettingSlip.Application.BettingSlips.Messaging;

namespace BettingSlip.Infrastructure.Messaging;

public class BetPublisher(IPublishEndpoint publishEndpoint) : IBetPublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task PublishBetPlaced(Guid betId, Guid userId, decimal amount)
    {
        var betPlaced = new BetPlaced
        {
            BetId = betId,
            UserId = userId,
            Amount = amount
        };

        await _publishEndpoint.Publish(betPlaced);
    }
}
