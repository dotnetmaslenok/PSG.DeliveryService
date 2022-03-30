using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Commands;
using PSG.DeliveryService.Application.Responses;

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

	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetOrderByIdAsync([FromQuery(Name = "o")] int orderId)
	{
		//TODO mediator query
		var result = await _mediator.Send(orderId);
		return Ok(Task.FromResult(new OrderResponse()).Result);
	}
	
	[Authorize(Policy = "Client")]
	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromForm] CreateOrderCommand createOrderCommand)
	{ 
		var result = await _mediator.Send(createOrderCommand);

		if (result.IsSuccess)
		{
			var actionName = nameof(GetOrderByIdAsync);
			return CreatedAtAction(actionName, new {Id = result.Value.ClientId}, result.Value);
		}

		return BadRequest(Results.Json(result.Error));
	}
}