using AutoMapper;
using FluentAssertions;
using Restaurant.Application.Restaurant.Commands.CreateRestaurant;
using Restaurant.Application.Restaurant.Commands.UpdateRestaurant;
using Restaurant.Domain.Entites;
using Xunit;

namespace Restaurant.Application.Restaurant.DTOS.Tests
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;
        public RestaurantsProfileTests() 
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

             _mapper = configuration.CreateMapper();
        }
        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapCorrectly()
        {
            // arrange
            

            var restaurant = new Restaurants()
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12-345"
                }
            };

            //act

            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            //assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            // arrange

            var command = new CreateRestaurantCommand()
            {
                Name = "Test restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12-345"
            };

            //act

            var restaurantDto = _mapper.Map<Restaurants>(command);

            //assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be(command.Name);
            restaurantDto.Description.Should().Be(command.Description);
            restaurantDto.Category.Should().Be(command.Category);
            restaurantDto.HasDelivery.Should().Be(command.HasDelivery);
            restaurantDto.ContactEmail.Should().Be(command.ContactEmail);
            restaurantDto.ContactNumber.Should().Be(command.ContactNumber);
            restaurantDto.Address.Should().NotBeNull();
            restaurantDto.Address.City.Should().Be(command.City);
            restaurantDto.Address.Street.Should().Be(command.Street);    
            restaurantDto.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            // arrange

            var command = new UpdateRestaurantCommand()
            {
                Id = 1 ,
                Name = "Update Restaurant",
                Description = "Update Restaurant Description",
                HasDelivery = false 
            };

            //act

            var restaurantDto = _mapper.Map<Restaurants>(command);

            //assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be(command.Name);
            restaurantDto.Id.Should().Be(command.Id);
            restaurantDto.Description.Should().Be(command.Description);
            restaurantDto.HasDelivery.Should().Be(command.HasDelivery);
        }


    }
}