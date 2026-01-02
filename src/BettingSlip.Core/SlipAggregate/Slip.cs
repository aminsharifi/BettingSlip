using BettingSlip.Core.Common;

namespace BettingSlip.Core.SlipAggregate;

public class Slip : BaseEntity
{
    public byte[] RowVersion { get; private set; } = default!;
    public decimal StakeAmount { get; private set; }
    public decimal TotalOdds { get; private set; }
    public decimal PotentialWin { get; private set; }
    public SlipStatus Status { get; private set; }

    private readonly List<Selection> _selections = [];
    public IEnumerable<Selection> Selections => _selections.AsReadOnly();

    private Slip() { } // EF Core

    public Slip(decimal stakeAmount)
    {
        if (stakeAmount <= 0)
            throw new ArgumentException("Stake amount must be greater than zero.");

        StakeAmount = stakeAmount;
        Status = SlipStatus.Draft;

        Recalculate();
    }

    public void AddSelection(Guid slipId, string eventName, string market, decimal odd)
    {
        EnsureDraft();

        var selection = new Selection(slipId, eventName, market, odd);
        _selections.Add(selection);

        Recalculate();
    }

    public void Submit()
    {
        EnsureDraft();

        if (_selections.Count == 0)
            throw new InvalidOperationException("Cannot submit a betting slip without selections.");

        Status = SlipStatus.Submitted;
    }

    private void Recalculate()
    {
        TotalOdds = _selections.Count != 0
            ? Selections.Aggregate(1m, (acc, s) => acc * s.Odd)
            : 1m;

        PotentialWin = StakeAmount * TotalOdds;
    }

    private void EnsureDraft()
    {
        if (Status != SlipStatus.Draft)
            throw new InvalidOperationException("Betting slip is already submitted.");
    }
}
