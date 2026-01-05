namespace BettingSlip.Contracts.Events
{
    public record BetPlaced
    {
        public Guid BetId { get; init; }
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public DateTime PlacedAt { get; init; } = DateTime.UtcNow;
    }
}