namespace BettingSlip.Application.BettingSlips.Commands;

public record AddSelectionCommand(
    Guid SlipId,
    string EventName,
    string Market,
    decimal Odd);
