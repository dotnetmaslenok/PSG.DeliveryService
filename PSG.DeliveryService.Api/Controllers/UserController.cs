using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSG.DeliveryService.Domain.Entities;

namespace PSG.DeliveryService.Api.Controllers
{
	[ApiController]
	[Route("api/user")]
	[Authorize]
	public class UserController
	{
		public UserController()
		{

		}

		[AllowAnonymous]
		[Route("login")]
		public string Login()
		{
			return "Login AllowAnonymous";
		}

		[Authorize(Policy = "Client")]
		[Route("someshit")]
		public string GetSomeShit()
		{
			return "Shit allow authorized";
		}

		[AllowAnonymous]
		[Route("orders")]
		public Order OrderInfo()
		{
			Order order = new Order();
			order.DeliveryPrice = 100;
			order.ProductPrice = 1000;
			order.OrderTime = DateTime.Now;
			return order;
		}

	}
}
