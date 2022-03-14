using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
	}
}
