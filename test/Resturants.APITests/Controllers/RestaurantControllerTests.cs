using Resturants.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Moq;
using Restaurant.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Restaurant.Domain.Entites;
using Resturants.APITests;
using System.Net.Http.Json;
using Restaurant.Application.Restaurant.DTOS;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Resturants.API.Controllers.Tests
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock = new();
        public RestaurantControllerTests(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IPolicyEvaluator , FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantsRepository), _ => _restaurantsRepositoryMock.Object));
                });
            });
        }


        [Fact()]
        public async Task GetAll_ForValidRequest_Return200OK()
        {
            //arrange
            var client = _applicationFactory.CreateClient();

            //act 
            var result = await client.GetAsync("api/restaurants?pageNumber=1&pageSize=10");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInvalidRequest_Return400BadRequest()
        {
            //arrange
            var client = _applicationFactory.CreateClient();

            //act 
            var result = await client.GetAsync("api/restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_ForNonExistingId_ShouldReutrn404NotFound()
        {
            //arrange
            var id = 1123;

            _restaurantsRepositoryMock.Setup(m => m.GetAsync(id)).ReturnsAsync((Restaurants?) null);

            var client = _applicationFactory.CreateClient();

            //act 
            var response = await client.GetAsync($"api/restaurants/{id}");

            //assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);

        }
        
        [Fact]
        public async Task GetById_ForExistingId_ShouldReutrn200OK()
        {
            //arrange
            var id = 99;

            var restauant = new Restaurants 
            {
                Id = id ,
                Name = "Test",
                Description = "Test description"
            };

            _restaurantsRepositoryMock.Setup(m => m.GetAsync(id)).ReturnsAsync(restauant);

            var client = _applicationFactory.CreateClient();

            //act 
            var response = await client.GetAsync($"api/restaurants/{id}");
            var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantDto>();

            //assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test description");


        }
    }
}