using BettingSlip.Contracts.Events;


namespace BettingSlip.WalletService.Consumers;

public class BetPlacedConsumer : IConsumer<BetPlaced>
{
    public async Task Consume(ConsumeContext<BetPlaced> context)
    {
        var message = context.Message;

        // Business logic: reserve funds in user's wallet
        Console.WriteLine($"Reserving {message.Amount} for user {message.UserId}");

        // TODO: call repository to update wallet balance
        await Task.CompletedTask;
    }
}