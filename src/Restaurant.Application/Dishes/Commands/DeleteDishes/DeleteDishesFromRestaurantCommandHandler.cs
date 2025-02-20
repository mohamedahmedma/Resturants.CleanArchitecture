using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.DeleteDishes
{
	public class DeleteDishesFromRestaurantCommandHandler : IRequestHandler<DeleteDishesFromRestaurantCommand>
	{
		private readonly ILogger _logger;
		private readonly IRestaurantsRepository _restaurantrepo;
		private readonly IDishesRepository _dishrepo;
		public DeleteDishesFromRestaurantCommandHandler(ILogger<DeleteDishesFromRestaurantCommandHandler> logger
			,IRestaurantsRepository restaurantsRepository , IDishesRepository dishesRepository)
		{
			_logger = logger;
			_restaurantrepo = restaurantsRepository;
			_dishrepo = dishesRepository;
		}
		public async Task Handle(DeleteDishesFromRestaurantCommand request, CancellationToken cancellationToken)
		{
			_logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.restaurantId);
			var restaurant = await _restaurantrepo.GetAsync(request.restaurantId);
			if(restaurant is null)
			{
				throw new NotFoundException(nameof(Restaurants), request.restaurantId.ToString());
			}
			await _dishrepo.Delete(restaurant.Dishes);
		}
	}
}
