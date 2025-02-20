using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Domain.Contants;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly ILogger<UpdateRestaurantCommandHandler> logger;
        private readonly IMapper mapper;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;
        private readonly IRestaurantsRepository restaurantsRepository;
        public UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository repository , IRestaurantAuthorizationService restaurantAuthorization)
        {
            this.logger = logger;
            this.mapper = mapper;
            restaurantsRepository = repository;
            restaurantAuthorizationService = restaurantAuthorization;
        }

        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Update Restaurant Id : {RestaurantId} with {@UpdatedRestaurant}" , request.Id , request);
            var restaurant = await restaurantsRepository.GetAsync(request.Id);
            if (restaurant is null)
            {
				throw new NotFoundException(nameof(Restaurants), request.Id.ToString());
			}
            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
            {
                throw new ForbidException();
            }
            var restuarant = mapper.Map(request, restaurant);
             await restaurantsRepository.SaveChanges();

        }
    }
}
