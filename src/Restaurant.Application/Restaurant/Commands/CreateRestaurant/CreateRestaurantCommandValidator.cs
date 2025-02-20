using FluentValidation;
using Restaurant.Application.Restaurant.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Restaurant.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandoValidator : AbstractValidator<CreateRestaurantCommand>
    {
        private readonly List<string> validCategories = ["Italian", "Maxican", "Japanese", "American", "Indian"];
        public CreateRestaurantCommandoValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category , Please choose from the valid categories");

            //RuleFor(dto => dto.Description)
            //	.NotEmpty().WithMessage("Description is required.");

            //RuleFor(x => x.Category)
            //	.NotEmpty().WithMessage("Insert a valid category");

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress().WithMessage("Please provide a valid email address");

            RuleFor(x => x.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX).");

        }

    }
}
