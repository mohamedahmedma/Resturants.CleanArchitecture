using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Commands.CreateDish
{
	public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand , int>
	{
		public ILogger<CreateDishCommandHandler> Logger { get; set; }
		public IRestaurantsRepository _restaurant { get; set; }
		public IDishesRepository _dishrepo { get; set; }
		public IMapper _mapper { get; set; }
		public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger , IRestaurantsRepository restaurants 
			, IDishesRepository dishesRepository , IMapper mapper) 
		{
			Logger = logger;
			_restaurant = restaurants;
			_dishrepo = dishesRepository;
			_mapper = mapper;
		}
		public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
		{
			Logger.LogInformation("Creating new dish: {@DishRequest}", request);
			var restaurant = await _restaurant.GetAsync(request.RestaurantId);
			if(restaurant == null)
			{
				throw new NotFoundException(nameof(Restaurants), request.RestaurantId.ToString());
			}
			var dish = _mapper.Map<Dish>(request);
			return await _dishrepo.Create(dish);
		}
	}
}
