using MediatR;
using Restaurant.Application.Dishes.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Queries.GetDishesForRestaurant
{
	public class GetDishesForRestaurantQuery : IRequest<IEnumerable<DishDto>>
	{
		public int restaurantId { get;}
		public GetDishesForRestaurantQuery(int restaurantid)
		{
			restaurantId = restaurantid;
		}
	}
}
