using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.Interfaces;
using PSG.DeliveryService.Application.ViewModels.OrderViewModels;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
	private readonly IOrderService _orderService;

	public OrderController(IOrderService orderService)
	{
		_orderService = orderService;
	}
	
	[HttpPost]
	public async Task<IActionResult> CreateOrder([FromForm] CreateOrderViewModel createOrderViewModel)
	{
		var orderId = await _orderService.CreateOrderAsync(createOrderViewModel);
		
	    //TODO: HOW?
        return Created(new Uri("https://localhost:7147/api/order"), orderId);
    }
}