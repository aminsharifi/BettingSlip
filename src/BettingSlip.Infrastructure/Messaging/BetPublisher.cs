using BettingSlip.Application.BettingSlips.Messaging;
using BettingSlip.Contracts.Events;
using BettingSlip.Core.SlipAggregate;
using MassTransit;

namespace BettingSlip.Infrastructure.Messaging;

public class BetPublisher(IPublishEndpoint publishEndpoint) : IBetPublisher
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

    public async Task PublishBetPlaced(Guid slipId, Guid userId, decimal amount)
    {
        var betPlaced = new BetPlaced
        {
            SlipId = slipId,
            UserId = userId,
            Amount = amount
        };

        await _publishEndpoint.Publish(betPlaced);
    }
}
