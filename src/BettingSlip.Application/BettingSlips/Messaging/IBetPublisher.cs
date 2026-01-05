namespace BettingSlip.Application.BettingSlips.Messaging;

public interface IBetPublisher
{
    Task PublishBetPlaced(Guid betId, Guid userId, decimal amount);
}