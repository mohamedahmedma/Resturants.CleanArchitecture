using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Domain.Contants;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.CreateRestaurant
{
	public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, int>
	{
		private readonly ILogger<CreateRestaurantCommandHandler> logger;
		private readonly IMapper mapper;
		private readonly IRestaurantsRepository restaurantsRepository;
		private readonly IUserContext userContext;
        public IRestaurantAuthorizationService restaurantAuthorizationService;

        public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger 
			, IMapper mapper, IRestaurantsRepository restaurantsRepository
			,IUserContext usercontext,IRestaurantAuthorizationService? RestaurantAuthorizationService)
		{
			this.logger = logger;
			this.mapper = mapper;
			this.restaurantsRepository = restaurantsRepository;
			this.userContext = usercontext;
			this.restaurantAuthorizationService = RestaurantAuthorizationService;
		}
		public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
		{
			var currentUser = userContext.GetCurrentUser();
			logger.LogInformation("{UserName} [{UserId}] is Creating a new restaurant {@Restaurants}" , currentUser.Email , currentUser.Id , request);
            var restaurant = mapper.Map<Restaurants>(request);
            restaurant.OwnerId = currentUser.Id;
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            {
                throw new ForbidException();
            }


			int id = await restaurantsRepository.Create(restaurant);
			return id;
		}
	}
}
