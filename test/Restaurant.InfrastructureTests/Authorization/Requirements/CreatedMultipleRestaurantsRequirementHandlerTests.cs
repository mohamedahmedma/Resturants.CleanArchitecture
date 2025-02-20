using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurant.Application.Users;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Repositories;
using Xunit;
namespace Restaurant.Infrastructure.Authorization.Requirements.Tests
{
    public class CreatedMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceedTest()
        {
            //arrange
            var currentUser = new CurrentUser("1" , "test@test.com", [] , null , null);
            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(u => u.GetCurrentUser())
                .Returns(currentUser);

            var restaurantsMock = new List<Restaurants>()
            {
                new()
                {
                    OwnerId = "2"

                },
                new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId =currentUser.Id
                }
            };

            var restaurantsRepoMock = new Mock<IRestaurantsRepository>();
            restaurantsRepoMock.Setup(u => u.GetAllAsync())
                .ReturnsAsync(restaurantsMock);

            var requirement = new CreatedMultipleRestaurantsRequirement(2);

            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepoMock.Object , userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);

            // act

            await handler.HandleAsync(context);

            //assert

            context.HasSucceeded.Should().BeTrue();
        }
        
        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFailTest()
        {
            //arrange
            var currentUser = new CurrentUser("1" , "test@test.com", [] , null , null);
            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(u => u.GetCurrentUser())
                .Returns(currentUser);

            var restaurantsMock = new List<Restaurants>()
            {
                new()
                {
                    OwnerId = "2"

                },
                new()
                {
                    OwnerId =currentUser.Id
                }
            };

            var restaurantsRepoMock = new Mock<IRestaurantsRepository>();
            restaurantsRepoMock.Setup(u => u.GetAllAsync())
                .ReturnsAsync(restaurantsMock);

            var requirement = new CreatedMultipleRestaurantsRequirement(2);

            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepoMock.Object , userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirement], null, null);

            // act

            await handler.HandleAsync(context);

            //assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();
        }
    }
}