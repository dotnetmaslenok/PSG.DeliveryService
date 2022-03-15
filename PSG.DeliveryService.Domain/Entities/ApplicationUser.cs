using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities
{
	public class ApplicationUser : IdentityUser<int>
	{
		public DateTime UserRegistrationTime { get; set; }

		public List<Order>? Orders { get; set; }
	}
}