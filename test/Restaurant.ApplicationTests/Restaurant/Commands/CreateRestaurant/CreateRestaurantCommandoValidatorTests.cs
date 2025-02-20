using FluentValidation.TestHelper;
using Microsoft.Extensions.Options;
using Xunit;

namespace Restaurant.Application.Restaurant.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandoValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345"
            };

            var validator = new CreateRestaurantCommandoValidator();

            //act


            var result = validator.TestValidate(command);

            //assert

            result.ShouldNotHaveAnyValidationErrors();
            
        }
        [Fact()]
        public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Category = "Ita",
                ContactEmail = "@test.com",
                PostalCode = "1345"
            };

            var validator = new CreateRestaurantCommandoValidator();

            //act


            var result = validator.TestValidate(command);

            //assert

            result.ShouldHaveValidationErrorFor(c => c.Name);
            result.ShouldHaveValidationErrorFor(c => c.Category);
            result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
            
        }

        [Theory()]
        [InlineData("Italian")]
        [InlineData("Maxican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
        {
            //arrange
            var validator = new CreateRestaurantCommandoValidator();
            var command = new CreateRestaurantCommand { Category = category };

            //act 
            var result = validator.TestValidate(command);
            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")]
        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCode(string postalCode)
        {
            //arrange
            var validator = new CreateRestaurantCommandoValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };

            //act 
            var result = validator.TestValidate(command);
            //assert
            result.ShouldHaveValidationErrorFor(c => c.Category);
        }

    }
}