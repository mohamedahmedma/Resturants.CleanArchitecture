using AutoMapper;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Domain.Entites;

namespace Restaurant.Application.Dishes.DTOS
{
	public class DishesProfile : Profile
	{
		public DishesProfile()
		{
			CreateMap<Dish, DishDto>().ReverseMap();
			CreateMap<CreateDishCommand, Dish>().ReverseMap();
		}
	}
}
