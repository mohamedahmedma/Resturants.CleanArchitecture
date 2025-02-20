using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Contants;


namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant
{
	public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
	{
		private readonly ILogger<DeleteRestaurantCommand> logger;
		private readonly IMapper mapper;
		private readonly IRestaurantsRepository restaurantsRepository;
		private readonly IRestaurantAuthorizationService restaurantAuthorizationService;
        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommand> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository , IRestaurantAuthorizationService restaurantAuthorization )
		{
			this.logger = logger;
			this.mapper = mapper;
			this.restaurantsRepository = restaurantsRepository;
            restaurantAuthorizationService = restaurantAuthorization;
        }

		public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Delete Restaurant Id = {RestaurantId}" , request.Id);
			var restaurant = await restaurantsRepository.GetAsync( request.Id );
			if ( restaurant is null )
			{
				throw new NotFoundException(nameof(Restaurants), request.Id.ToString());
			}
			if(!restaurantAuthorizationService.Authorize(restaurant , ResourceOperation.Delete))
			{
				throw new ForbidException();
			}
			await restaurantsRepository.Delete(restaurant);
		}
	}
}
