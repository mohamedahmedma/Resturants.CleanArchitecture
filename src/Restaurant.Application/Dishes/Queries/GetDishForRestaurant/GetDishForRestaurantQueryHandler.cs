using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Queries.GetDishForRestaurant
{
	public class GetDishForRestaurantQueryHandler : IRequestHandler<GetDishForRestaurantQuery, DishDto>
	{
		private readonly ILogger<GetDishForRestaurantQueryHandler> _logger;
		private readonly IRestaurantsRepository _restaurantsRepository;
		private readonly IMapper _mapper;
		public GetDishForRestaurantQueryHandler(ILogger<GetDishForRestaurantQueryHandler> logger, IRestaurantsRepository restaurantsRepository, IMapper mapper)
		{
			_logger = logger;
			_restaurantsRepository = restaurantsRepository;
			_mapper = mapper;
		}

		public async Task<DishDto> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Retrieving dish: {DishId} for restaurant with id : {RestaurantId}"
				,request.DishId
				, request.restaurantId);
			var restaurant = await _restaurantsRepository.GetAsync(request.restaurantId);
			if (restaurant == null)
			{
				throw new NotFoundException(nameof(Restaurants), request.restaurantId.ToString());
			}
			var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
			if (dish == null)
			{
				throw new NotFoundException(nameof(Dish), request.DishId.ToString());
			}
			var result = _mapper.Map<DishDto>(dish);
			return result;
		}
	}
}
