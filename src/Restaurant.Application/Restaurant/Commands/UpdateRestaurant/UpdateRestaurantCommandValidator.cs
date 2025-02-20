﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);
        }
    }
}
