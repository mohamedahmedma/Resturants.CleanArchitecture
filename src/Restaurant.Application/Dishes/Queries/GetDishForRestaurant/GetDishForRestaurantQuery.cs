using MediatR;
using Restaurant.Application.Dishes.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Queries.GetDishForRestaurant
{
	public class GetDishForRestaurantQuery : IRequest<DishDto>
	{
		public int restaurantId { get; }
		public int DishId { get; }
		public GetDishForRestaurantQuery(int restaurantId, int dishId)
		{
			this.restaurantId = restaurantId;
			DishId = dishId;
		}
	}
}
