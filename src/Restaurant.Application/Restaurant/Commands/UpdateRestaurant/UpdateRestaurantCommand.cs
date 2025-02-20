using MediatR;
using Restaurant.Application.Restaurant.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }

    }
}
