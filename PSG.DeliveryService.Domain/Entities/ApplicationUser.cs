using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Guid Id { get; set; }
		public DateTime UserRegistrationTime { get; set; }

		public List<Order>? CustomerOrders { get; set; }

		public List<Order>? CourierOrders { get; set; }

		public bool IsCourier { get; set; }

		public ApplicationUser()
		{
			CustomerOrders = new List<Order>();
			CourierOrders = new List<Order>();
		}
	}
}