using System.ComponentModel.DataAnnotations.Schema;
using PSG.DeliveryService.Domain.Enums;

namespace PSG.DeliveryService.Domain.Entities
{
	public class Order
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public string? ProductName { get; set; }

		public OrderType OrderType { get; set; }

		public DateTime? OrderTime { get; set; }

		public decimal TotalPrice { get; set; }

		public double Distance { get; set; }
		
		public string? DeliveryAddress { get; set; }

		public string? ProductAddress { get; set; }

		public OrderWeight OrderWeight { get; set; }

		public DeliveryType DeliveryType { get; set; }
		
		public OrderState OrderState { get; set; }
		
		public Guid? CustomerId { get; set; }

		public ApplicationUser? Customer { get; set; }
		
		public Guid? CourierId { get; set; }

		public ApplicationUser? Courier { get; set; }
	}
}
