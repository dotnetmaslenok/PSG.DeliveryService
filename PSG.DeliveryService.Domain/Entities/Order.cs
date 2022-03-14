using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSG.DeliveryService.Domain.Entities
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public DateTime OrderTime { get; set; }

		public decimal ProductPrice { get; set; }

		public decimal DeliveryPrice { get; set; }

		public string DeliveryAddress { get; set; }

		public string ProductAddress { get; set; }

		public decimal OrderWeight { get; set; }

		public enum DeliveryType
		{
			[Description("On foot")]
			OnFoot,
			Car,
			Truck
		}

		[ForeignKey("Customer")]
		public int CustomerId { get; set; }

		public ApplicationUser Customer { get; set; }
	}
}
