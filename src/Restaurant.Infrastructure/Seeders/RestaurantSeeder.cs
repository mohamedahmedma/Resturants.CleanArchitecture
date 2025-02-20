
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Contants;
using Restaurant.Domain.Entites;
using Restaurant.Infrastructure.Persistence;

namespace Restaurant.Infrastructure.Seeders
{
	public class RestaurantSeeder : IRestaurantSeeder
	{
		private readonly RestaurantDbContext _context;
		public RestaurantSeeder(RestaurantDbContext context)
		{
			_context = context;
		}
		public async Task Seed()
		{
			if (await _context.Database.CanConnectAsync())
			{
				if (!_context.Restaurants.Any())
				{
					var restaurants = GetRestaurants();
					_context.Restaurants.AddRange(restaurants);
					await _context.SaveChangesAsync();
				}
				if (!_context.Roles.Any())
				{
					var roles = GetRoles();
					_context.Roles.AddRange(roles);
					await _context.SaveChangesAsync();
				}
			}
		}

		private IEnumerable<IdentityRole> GetRoles()
		{
			List<IdentityRole> roles =
				[
					new(UserRole.User)
					{
						NormalizedName = UserRole.User.ToUpper()
					},
					new(UserRole.Owner)
					{
						NormalizedName = UserRole.Owner.ToUpper()
					},
					new(UserRole.Admin)
					{
						NormalizedName = UserRole.Admin.ToUpper()
					},
				];
			return roles;
		}

		private IEnumerable<Restaurants> GetRestaurants()
		{
			List<Restaurants> restaurants = [
				new()
			{
				Name = "KFC",
				Category = "Fast Food",
				OwnerId = "90cd607f-3360-4433-b124-9a3bad65f0be",
				Description =
					"KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
				ContactEmail = "contact@kfc.com",
				HasDelivery = true,
				Dishes =
				[
					new ()
					{
						Name = "Nashville Hot Chicken",
						Description = "Nashville Hot Chicken (10 pcs.)",
						Price = 10.30M,
					},

					new ()
					{
						Name = "Chicken Nuggets",
						Description = "Chicken Nuggets (5 pcs.)",
						Price = 5.30M,
					},
				],
				Address = new ()
				{
					City = "London",
					Street = "Cork St 5",
					PostalCode = "WC2N 5DU"
				}
			},
				new ()
			{
				Name = "McDonald",
				Category = "Fast Food",
				OwnerId = "90cd607f-3360-4433-b124-9a3bad65f0be",

                Description =
					"McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
				ContactEmail = "contact@mcdonald.com",
				HasDelivery = true,
				Address = new Address()
				{
					City = "London",
					Street = "Boots 193",
					PostalCode = "W1F 8SR"
				}
			}
			];
			return restaurants;
		}
	}
}