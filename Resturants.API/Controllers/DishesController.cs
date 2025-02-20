﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands.CreateDish;
using Restaurant.Application.Dishes.Commands.DeleteDishes;
using Restaurant.Application.Dishes.DTOS;
using Restaurant.Application.Dishes.Queries.GetDishesForRestaurant;
using Restaurant.Application.Dishes.Queries.GetDishForRestaurant;
using Restaurant.Infrastructure.Authorization;

namespace Resturants.API.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
        [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            await mediator.Send(new DeleteDishesFromRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
