namespace BettingSlip.Application.BettingSlips.DTOs;

public record SelectionDto(
  Guid Id,
  string EventName,
  string Market,
  decimal Odd);
