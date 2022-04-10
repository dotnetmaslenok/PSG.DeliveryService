using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Queries;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/order")]
public sealed class OrderController : ControllerBase
{
	private readonly IMediator _mediator;

	public OrderController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet]
	[Authorize("Bearer")]
	public async Task<IActionResult> GetByIdAsync([FromQuery(Name = "o")] string orderId)
	{
		var orderQuery = new OrderQuery(orderId);
		var result = await _mediator.Send(orderQuery);

		if (result.Value is null)
		{
			return NotFound();
		}
		
		return Ok(result.Value);
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