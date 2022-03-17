﻿using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities
{
	public class ApplicationUser : IdentityUser<int>
	{
		public DateTime UserRegistrationTime { get; set; }

		public List<Order>? CustomerOrders { get; set; }

		public List<Order>? CourierOrders { get; set; }
	}
}