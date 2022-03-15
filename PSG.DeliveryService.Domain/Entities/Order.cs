using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PSG.DeliveryService.Domain.Enums;

namespace PSG.DeliveryService.Domain.Entities
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		public DateTime OrderTime { get; set; }

		public decimal ProductPrice { get; set; }

		public decimal DeliveryPrice { get; set; }

		public string? DeliveryAddress { get; set; }

		public string? ProductAddress { get; set; }

		public decimal OrderWeight { get; set; }

		public DeliveryType DeliveryType { get; set; }
		
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }

		public ApplicationUser? Customer { get; set; }

		[ForeignKey("Courier")]
		public int CourierId { get; set; }

		public Courier? Courier { get; set; }
	}
}
