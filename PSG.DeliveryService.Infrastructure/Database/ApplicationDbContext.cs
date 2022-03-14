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
		public DbSet<Courier> Couriers { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Order>()
				.Property(x => x.ProductPrice)
				.HasPrecision(8, 2);
			
			builder.Entity<Order>()
				.Property(x => x.DeliveryPrice)
				.HasPrecision(8, 2);
			
			builder.Entity<Order>()
				.Property(x => x.OrderWeight)
				.HasPrecision(8, 3);

			base.OnModelCreating(builder);
		}
	}
}
