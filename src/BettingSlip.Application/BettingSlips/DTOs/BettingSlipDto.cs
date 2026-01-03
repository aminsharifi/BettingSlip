using BettingSlip.Core.SlipAggregate;

namespace BettingSlip.Application.BettingSlips.DTOs;

public record BettingSlipDto(
  Guid Id,
  decimal StakeAmount,
  decimal TotalOdds,
  decimal PotentialWin,
  string Status,
  IReadOnlyCollection<SelectionDto> Selections);

public static class BettingSlipDtoExtensions
{
    public static BettingSlipDto ToBettingSlipDto(this Slip slip)
    {
        return new BettingSlipDto(
          slip.Id,
          slip.StakeAmount,
          slip.TotalOdds,
          slip.PotentialWin,
          slip.Status.ToString(),
          [.. slip.Selections.Select(s =>
                new SelectionDto(s.Id, s.EventName, s.Market, s.Odd))]);
    }
}