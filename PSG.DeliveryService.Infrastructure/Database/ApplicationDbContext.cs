using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Domain.Authorization;
using PSG.DeliveryService.Domain.Entities;

namespace PSG.DeliveryService.Infrastructure.Database
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Order>()
				.Property(x => x.ProductPrice)
				.HasPrecision(8, 2);
			
			builder.Entity<Order>()
				.Property(x => x.DeliveryPrice)
				.HasPrecision(8, 2);

			builder.Entity<Order>()
				.HasOne(m => m.Customer)
				.WithMany(t => t.CustomerOrders)
				.HasForeignKey(m => m.CustomerId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Order>()
				.HasOne(m => m.Courier)
				.WithMany(t => t.CourierOrders)
				.HasForeignKey(m => m.CourierId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}
	}
}
