using Restaurant.Domain.Entites;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Infrastructure.Repositories
{
	public class DishesRepository : IDishesRepository
	{
		public RestaurantDbContext DbContext { get; set; }
		public DishesRepository(RestaurantDbContext dbContext)
		{
			DbContext = dbContext;
		}
		public async Task<int> Create(Dish entity)
		{
			DbContext.Dishes.Add(entity);
			await DbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(IEnumerable<Dish> entites)
		{
			DbContext.Dishes.RemoveRange(entites);
			await DbContext.SaveChangesAsync();
		}
	}
}
