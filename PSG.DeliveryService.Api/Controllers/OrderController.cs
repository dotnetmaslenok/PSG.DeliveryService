using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Application.ViewModels;
using PSG.DeliveryService.Domain.Entities;
using PSG.DeliveryService.Domain.Enums;
using PSG.DeliveryService.Infrastructure.Database;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController : ControllerBase
{
	private readonly ApplicationDbContext _dbContext;

	public OrderController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	/*
	{
		"OrderType": int,
		"OrderTime": твоя дата,
		"DeliveryType": int,
		"OrderWeight": int,
		"From":string,
		"To": string,
		"ProductName":string
	}
	*/
	[HttpPost]
	[AllowAnonymous]
	[Route("create")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderViewModel createOrderViewModel)
    {
	    const int fastDeliveryTime = 30;

	    int.TryParse(createOrderViewModel.OrderWeight, out int orderWeight);
	    int.TryParse(createOrderViewModel.OrderType, out int orderType);
	    int.TryParse(createOrderViewModel.DeliveryType, out int deliveryType);
	    
	    var order = new Order
	    {
	        OrderWeight = (OrderWeight)orderWeight,
	        OrderType = (OrderType)orderType,
	        DeliveryType = (DeliveryType)deliveryType,
	        ProductName = createOrderViewModel.ProductName,
            ProductAddress = createOrderViewModel.From,
            DeliveryAddress = createOrderViewModel.To,
            OrderTime = !string.IsNullOrEmpty(createOrderViewModel.OrderTime) ? Convert.ToDateTime(createOrderViewModel.OrderTime) : DateTime.UtcNow.AddMinutes(fastDeliveryTime)
	    };

	    await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        return new JsonResult(order);
    }
}