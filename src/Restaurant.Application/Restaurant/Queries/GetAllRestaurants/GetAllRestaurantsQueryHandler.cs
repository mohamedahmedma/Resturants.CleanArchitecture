using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurant.DTOS;
using Restaurant.Domain.Repositories;


namespace Restaurant.Application.Restaurant.Queries.GetAllRestaurants
{
	public class GetAllRestaurantsQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
	{
		private readonly ILogger<GetAllRestaurantsQueryHandler> logger;
		private readonly IMapper mapper;
		private readonly IRestaurantsRepository restaurantsRepository;
		public GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository)
		{
			this.logger = logger;
			this.mapper = mapper;
			this.restaurantsRepository = restaurantsRepository;
		}

		public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all restaurants");

			var (resturants, totalCount) = await restaurantsRepository.GetAllMatchesAsync(request.SearchPhrase,
				request.PageSize, request.PageNumber, request.SortBy, request.SortDirection ?? SortDirection.Ascending);

			var resturantsDto = mapper.Map<IEnumerable<RestaurantDto>>(resturants);
			var result = new PagedResult<RestaurantDto>(resturantsDto ,totalCount , request.PageSize , request.PageNumber);
			return result;
		}
	}
}
