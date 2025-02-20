using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace Restaurant.Infrastructure.Repositories
{
	public class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
	{
		public async Task<int> Create(Restaurants entity)
		{
			dbContext.Restaurants.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task Delete(Restaurants entity)
		{
			dbContext.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Restaurants>> GetAllAsync()
		{
			var restaurants = await dbContext.Restaurants
				.Include(r => r.Dishes)
				.ToListAsync();
			return restaurants;
		}
		public async Task<(IEnumerable<Restaurants> , int)> GetAllMatchesAsync(string? searchPhrase, int pageSize , int pageNumber ,string? sortBy , SortDirection? sort )
		{
			var searchPhraseLower = searchPhrase?.ToLower();

			var baseQuery = dbContext.Restaurants
				.Include(r => r.Dishes)
				.Where(u => searchPhrase == null ||
				(u.Name.ToLower().Contains(searchPhraseLower) ||
				u.Description.ToLower().Contains(searchPhraseLower))
				);

			var totalCount = await baseQuery.CountAsync();

			if(sortBy != null)
			{
				var columnSelector = new Dictionary<string, Expression<Func<Restaurants, object>>>()
				{
					{nameof(Restaurants.Name), r => r.Name },
					{nameof(Restaurants.Description), r => r.Description },
					{nameof(Restaurants.Category), r => r.Category }
				};

				var selectedColumn = columnSelector[sortBy];
				baseQuery =
					sort == SortDirection.Ascending ?
					baseQuery.OrderBy(selectedColumn) : baseQuery.OrderByDescending(selectedColumn);
			}


            var restaurants = await baseQuery
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToListAsync();
			return (restaurants , totalCount);
		}

		public async Task<Restaurants> GetAsync(int id)
		{
		var restaurant = await dbContext.Restaurants
				.Include(r => r.Dishes)
				.FirstOrDefaultAsync(x => x.Id == id);

			return restaurant;
		}

		public async Task SaveChanges()
		{
			await dbContext.SaveChangesAsync();
		}
	}
}
