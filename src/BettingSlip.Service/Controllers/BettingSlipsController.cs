namespace BettingSlip.Service.Controllers;

using BettingSlip.Application.BettingSlips.Commands;
using BettingSlip.Application.BettingSlips.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/betting-slips")]
public class BettingSlipsController(BettingSlipService service) : ControllerBase
{

    /// <summary>Create a new betting slip</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBettingSlipCommand command)
    {
        var id = await service.CreateAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    /// <summary>Add selection to betting slip</summary>
    [HttpPost("{id:guid}/selections")]
    public async Task<IActionResult> AddSelection(
        Guid id,
        [FromBody] AddSelectionCommand command)
    {
        await service.AddSelectionAsync(command);

        return NoContent();
    }

    /// <summary>Submit betting slip</summary>
    [HttpPost("{id:guid}/submit")]
    public async Task<IActionResult> Submit(Guid id)
    {
        await service.SubmitAsync(new SubmitBettingSlipCommand(id));
        return NoContent();
    }

    /// <summary>Get betting slip</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await service.GetAsync(id);
        return result is null ? NotFound() : Ok(result);
    }
    /// <summary>Get betting slip</summary>
    [HttpGet]
    public async Task<IActionResult> ResultAsync()
    {
        var result = await service.ResultAsync();
        return  Ok(result);
    }
}

