using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Queries.OrderQueries;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/orders")]
public sealed class OrderController : ControllerBase
{
	private readonly IMediator _mediator;

	public OrderController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet("{orderId}")]
	[Authorize("Bearer")]
	public async Task<IActionResult> GetByIdAsync([FromRoute] string orderId)
	{
		var query = new GetOneOrderQuery(orderId);
		var result = await _mediator.Send(query);

		if (result.Value is null)
		{
			return NotFound();
		}
		
		return Ok(result.Value);
	}

	[HttpGet]
	[Authorize("Bearer")]
	public async Task<IActionResult> GetAll()
	{
		var query = new GetManyOrderQuery();
		return Ok(await _mediator.Send(query));
	}
	
	[HttpPost]
	[Authorize(Policy="Client")]
	public async Task<IActionResult> CreateAsync([FromForm] CreateOrderCommand createOrderCommand)
	{ 
		var result = await _mediator.Send(createOrderCommand);

		if (result.IsFailure)
		{
			return BadRequest(result.Value);
		}
		
		return Created(Request.Path, result.Value);
	}
}