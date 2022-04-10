using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PSG.DeliveryService.Domain.Entities
{
	public sealed class ApplicationRole : IdentityRole<Guid>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override Guid Id { get; set; }
	}
}