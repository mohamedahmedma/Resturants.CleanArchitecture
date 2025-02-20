using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Application.Restaurant.Queries.GetAllRestaurants;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Queries.GetRestaurant
{
	public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, RestaurantDto>
	{
		private readonly ILogger<GetAllRestaurantsQueryHandler> logger;
		private readonly IMapper mapper;
		private readonly IRestaurantsRepository restaurantsRepository;
		public GetRestaurantQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository)
		{
			this.logger = logger;
			this.mapper = mapper;
			this.restaurantsRepository = restaurantsRepository;
		}

		public async Task<RestaurantDto> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Get restaurant where id = {heke}", request.Id);
			var restaurant = await restaurantsRepository.GetAsync(request.Id)
				?? throw new NotFoundException(nameof(Restaurants) , request.Id.ToString());
			
			var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
			return restaurantDto;
		}
	}
}
