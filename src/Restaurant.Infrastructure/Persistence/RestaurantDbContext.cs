using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entites;

namespace Restaurant.Infrastructure.Persistence
{
	public class RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : IdentityDbContext<User>(options)
	{
		public DbSet<Restaurants> Restaurants { get; set; }
		public DbSet<Dish> Dishes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Restaurants>()
				.OwnsOne(r => r.Address);

			modelBuilder.Entity<Restaurants>()
				.HasMany(r => r.Dishes)
				.WithOne()
				.HasForeignKey(d => d.RestaurantId);

			modelBuilder.Entity<User>()
				.HasMany(o => o.OwendRestaurants)
				.WithOne(r => r.Owner)
				.HasForeignKey(d => d.OwnerId);
		}
	}

}

