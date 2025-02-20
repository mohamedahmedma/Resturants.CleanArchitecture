using MediatR;
using Restaurant.Application.Restaurant.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Queries.GetRestaurant
{
	public class GetRestaurantQuery : IRequest<RestaurantDto>
	{
		public int Id { get; set; }

		public GetRestaurantQuery(int id)
		{
			Id = id;
		}
	}
}
