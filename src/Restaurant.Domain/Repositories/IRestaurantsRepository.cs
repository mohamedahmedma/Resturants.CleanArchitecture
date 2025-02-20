using Restaurant.Application.Common;
using Restaurant.Domain.Entites;

namespace Restaurant.Domain.Repositories
{
	public interface IRestaurantsRepository
	{
		Task<Restaurants> GetAsync(int id);
		Task<IEnumerable<Restaurants>> GetAllAsync();
		Task<int> Create(Restaurants entity);
		Task Delete(Restaurants entity);
		Task SaveChanges();
		Task<(IEnumerable<Restaurants>, int)> GetAllMatchesAsync(string? searchPhrase , int pageSize , int pageNumber ,string? sortBy , SortDirection? sortDirection );

    }
}
