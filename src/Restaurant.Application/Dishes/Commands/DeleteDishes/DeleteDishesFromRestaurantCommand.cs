using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes
{
	public class DeleteDishesFromRestaurantCommand : IRequest
	{
		public int restaurantId {  get; set; }
		public DeleteDishesFromRestaurantCommand(int restaurantId)
		{
			this.restaurantId = restaurantId;
		}
	}
}
