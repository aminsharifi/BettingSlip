using MassTransit;
using BettingSlip.Contracts.Events;

namespace BettingSlip.Infrastructure.Messaging;

public class BetSaga : MassTransitStateMachine<BetSagaState>
{
    public State WalletPending { get; private set; }
    public State Completed { get; private set; }
    public Event<BetPlaced> BetPlaced { get; private set; }
    public Event<WalletReserved> WalletReserved { get; private set; }
    public Event<WalletRejected> WalletRejected { get; private set; }

    [Obsolete]
    public BetSaga()
    {
        InstanceState(x => x.CurrentState);

        Initially(
            When(BetPlaced)
                .Then(context =>
                {
                    context.Instance.SlipId = context.Data.SlipId;
                    context.Instance.Amount = context.Data.Amount;
                })
                .Publish(context => new ReserveWallet(
                    context.Instance.SlipId,
                    context.Instance.Amount))
                .TransitionTo(WalletPending)
        );

        During(WalletPending,
            When(WalletReserved)
                .Publish(context => new ConfirmBet(context.Instance.SlipId))
                .TransitionTo(Completed),

            When(WalletRejected)
                .Publish(context => new RejectBet(context.Instance.SlipId))
                .TransitionTo(Completed)
        );
    }
}