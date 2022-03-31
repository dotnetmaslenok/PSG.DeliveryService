using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSG.DeliveryService.Domain.Entities;

namespace PSG.DeliveryService.Infrastructure.Database
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Order>()
				.HasKey(x => x.Id);

			builder.Entity<ApplicationUser>()
				.HasKey(x => x.Id);

			builder.Entity<ApplicationRole>()
				.HasKey(x => x.Id);

			builder.Entity<Order>()
				.HasOne(x => x.Customer)
				.WithMany(x => x.CustomerOrders)
				.HasForeignKey(x => x.CustomerId)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.Entity<Order>()
				.HasOne(x => x.Courier)
				.WithMany(x => x.CourierOrders)
				.HasForeignKey(x => x.CourierId)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.Entity<Order>()
				.Property(x => x.TotalPrice)
				.HasPrecision(8, 2);

			base.OnModelCreating(builder);
		}
	}
}
