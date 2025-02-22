using Xunit;

using Moq;
using AutoMapper;
using Restaurant.Domain.Repositories;
using Restaurant.Domain.Entites;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using FluentAssertions;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Restaurant.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnCreatedRestaurantId()
        {
            //arrange
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();

            var restaurant = new Restaurants();

            var restaurantAuth = new Mock<IRestaurantAuthorizationService>();

            mapperMock.Setup(m => m.Map<Restaurants>(command)).Returns(restaurant);

            var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();

            restaurantRepositoryMock.Setup(repo => repo.Create(It.IsAny<Restaurants>()))
                .ReturnsAsync(1);

            var userContextMock = new Mock<IUserContext>();

            var currentuser = new CurrentUser("Owner-id", "test@test.com", [], null, null);

            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentuser);

            var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object 
                , mapperMock.Object 
                , restaurantRepositoryMock.Object
                , userContextMock.Object
                , restaurantAuth.Object);

            restaurantAuth.Setup(u => u.Authorize(restaurant, Domain.Contants.ResourceOperation.Create))
                .Returns(true);

            //act 
            var result = await commandHandler.Handle(command , CancellationToken.None);

            // assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("Owner-id");
            restaurantRepositoryMock.Verify(r => r.Create(restaurant) , Times.Once);
        }
    }
}