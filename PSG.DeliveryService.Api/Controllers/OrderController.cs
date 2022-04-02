using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Queries;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
	private readonly IMediator _mediator;

	public OrderController(IMediator mediator)
	{
		_mediator = mediator;
	}
	
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> GetOrderByIdAsync([FromQuery(Name = "o")] string orderId)
	{
		var orderQuery = new OrderQuery(orderId);
		var result = await _mediator.Send(orderQuery);

		if (result.Value is not null)
		{
			return Ok(result.Value);
		}

		return NotFound();
	}
	
	[HttpPost]
	[Authorize(Policy="Client")]
	public async Task<IActionResult> CreateOrderAsync([FromForm] CreateOrderCommand createOrderCommand)
	{ 
		var result = await _mediator.Send(createOrderCommand);

		if (result.IsSuccess)
		{
			return Created(Request.Path, result);
		}

		return BadRequest(result.Value);
	}
}