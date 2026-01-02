using BettingSlip.Core.Common;

namespace BettingSlip.Core.SlipAggregate;

public class Selection : BaseEntity
{
    public string EventName { get; private set; } = null!;
    public string Market { get; private set; } = null!;
    public decimal Odd { get; private set; }
    public Guid SlipId { get; private set; }

    private Selection() { }
    public Selection(Guid slipId, string eventName, string market, decimal odd)
    {
        if (string.IsNullOrWhiteSpace(eventName))
            throw new ArgumentException("Event name is required.");

        if (string.IsNullOrWhiteSpace(market))
            throw new ArgumentException("Market is required.");

        if (odd <= 1)
            throw new ArgumentException("Odd must be greater than 1.");

        SlipId = slipId;
        EventName = eventName;
        Market = market;
        Odd = odd;
    }
}
