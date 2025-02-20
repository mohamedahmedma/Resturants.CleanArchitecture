using AutoMapper;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Application.Restaurant.Commands.UpdateRestaurant;
using Restaurant.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.DTOS
{
    public class RestaurantsProfile : Profile
	{
		public RestaurantsProfile() 
		{
			CreateMap<Restaurants, RestaurantDto>()
				.ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
				.ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
				.ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
				.ForMember(d => d.Dishes , opt => opt.MapFrom(src => src.Dishes ))
				.ReverseMap();

			CreateMap<CreateRestaurantCommand, Restaurants>()
				.ForMember(d => d.Address, opt => opt.MapFrom(src => new Address
				{
					City = src.City,
					PostalCode = src.PostalCode,
					Street = src.Street,
				}))
				.ReverseMap();

			CreateMap<Restaurants , UpdateRestaurantCommand>().ReverseMap();

		}

	}
}
