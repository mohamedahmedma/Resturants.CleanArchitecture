using Restaurant.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Repositories
{
	public interface IDishesRepository
	{
		Task<int> Create(Dish entity);
		Task Delete( IEnumerable<Dish> entites);

	}
}
