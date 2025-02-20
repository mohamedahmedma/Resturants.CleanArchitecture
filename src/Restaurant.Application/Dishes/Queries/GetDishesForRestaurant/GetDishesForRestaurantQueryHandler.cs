using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.GetDishesForRestaurant
{
	public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
	{
		private readonly ILogger<GetDishesForRestaurantQueryHandler> _logger;
		private readonly IRestaurantsRepository _restaurantsRepository;
		private readonly IMapper _mapper;
		public GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger , IRestaurantsRepository restaurantsRepository
			,IMapper mapper)
		{
			_logger = logger;
			_restaurantsRepository = restaurantsRepository;
			_mapper = mapper;

		}
		public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Retrieving dishes for restaurant with id : {RestaurantId}", request.restaurantId);
			var restaurant = await _restaurantsRepository.GetAsync(request.restaurantId);
			if (restaurant == null)
			{
				throw new NotFoundException(nameof(Restaurants),request.restaurantId.ToString());
			}
			var result = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
			return result;
		}
	}
}
