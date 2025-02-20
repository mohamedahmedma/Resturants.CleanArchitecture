using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurant.DTOS;


namespace Restaurant.Application.Restaurant.Queries.GetAllRestaurants
{
	public class GetAllRestaurantsQuery :IRequest<PagedResult<RestaurantDto>>
	{
		public string? SearchPhrase { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public string? SortBy { get; set; }
		public SortDirection? SortDirection { get; set; }
	}
}
