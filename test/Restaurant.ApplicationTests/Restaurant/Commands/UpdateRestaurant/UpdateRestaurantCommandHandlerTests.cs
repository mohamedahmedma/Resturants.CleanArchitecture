using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Xunit;

namespace Restaurant.Application.Restaurant.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantsRepository> _restaurantRepository;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuth;
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests( )
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _restaurantRepository =new Mock < IRestaurantsRepository >();
            _mapperMock = new Mock<IMapper> ();
            _restaurantAuth = new Mock < IRestaurantAuthorizationService > ();
            _handler = new UpdateRestaurantCommandHandler(
                _loggerMock.Object,
                _mapperMock.Object,
                _restaurantRepository.Object,
                _restaurantAuth.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand()
            {
                Id = restaurantId,
                Name = "New Test Name",
                Description = "New Test Description",
                HasDelivery = true
            };

            var restaurant = new Restaurants()
            {
                Id = restaurantId,
                Name = "Test",
                Description = "Test"
            };

            _restaurantRepository.Setup(u => u.GetAsync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuth.Setup(u => u.Authorize(restaurant, Domain.Contants.ResourceOperation.Update))
                .Returns(true);

            //act
            await _handler.Handle(command, CancellationToken.None);

            //assert
            _restaurantRepository.Verify(r => r.SaveChanges(), Times.Once);
            _mapperMock.Verify(r => r.Map(command , restaurant) , Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
        {
            var restaurantId = 2;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId,
            };
            _restaurantRepository.Setup(r => r.GetAsync(restaurantId))
                .ReturnsAsync((Restaurants?)null);

            _restaurantRepository.Setup(r => r.GetAsync(restaurantId))
                .ReturnsAsync((Restaurants?)null);

            //act

            Func<Task> act = async () =>
                await _handler.Handle(request , CancellationToken.None);
            //assert 
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurants with id : {restaurantId} doesn't exist");
        }

        [Fact]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            //Arrange
            var restaurantId = 3;
            var request = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };
            var existingRestaurant = new Restaurants
            {
                Id = restaurantId
            };

            _restaurantRepository.Setup(r => r.GetAsync(restaurantId)).ReturnsAsync(existingRestaurant);

            _restaurantAuth.Setup(u => u.Authorize(existingRestaurant, Domain.Contants.ResourceOperation.Update))
                .Returns(false);

            Func<Task> act = async () =>
            {
                await _handler.Handle(request, CancellationToken.None);
            };

            //act 
            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}