

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurant.Commands.DeleteRestaurant
{
	public class DeleteRestaurantCommand : IRequest
	{
		public int Id { get; set; }
		public DeleteRestaurantCommand(int id)
		{
			Id = id;
		}
	}
}
