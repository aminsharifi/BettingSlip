namespace BettingSlip.Application.BettingSlips.DTOs;

public record BettingSlipDto(
  Guid Id,
  decimal StakeAmount,
  decimal TotalOdds,
  decimal PotentialWin,
  string Status,
  IReadOnlyCollection<SelectionDto> Selections);