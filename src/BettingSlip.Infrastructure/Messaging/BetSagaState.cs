using MassTransit;

namespace BettingSlip.Infrastructure.Messaging;

public class BetSagaState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; } = default!;
    public Guid SlipId { get; set; }
    public decimal Amount { get; set; }
   
    public byte[] RowVersion { get; set; }
}
