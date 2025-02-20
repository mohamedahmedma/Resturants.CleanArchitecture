
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Authorization;
using Restaurant.Infrastructure.Authorization.Requirements;
using Restaurant.Infrastructure.Authorization.Services;
using Restaurant.Infrastructure.Persistence;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Seeders;

namespace Restaurant.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddInfrastructre(this IServiceCollection services , IConfiguration configuration)
		{
			var connectionstring = configuration.GetConnectionString("RestaurantDb");
			services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(connectionstring)
				.EnableSensitiveDataLogging());

			services.AddScoped<IRestaurantSeeder , RestaurantSeeder>();
			services.AddScoped<IRestaurantsRepository , RestaurantsRepository>();
			services.AddScoped<IDishesRepository , DishesRepository>();
			services.AddIdentityApiEndpoints<User>()
				.AddRoles<IdentityRole>()
				.AddClaimsPrincipalFactory<RestaurantUserClaimsPrincipalFactory>()
				.AddEntityFrameworkStores<RestaurantDbContext>();
			services.AddAuthorizationBuilder()
				.AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "EGY" , "German"))
				.AddPolicy(PolicyNames.AtLeast20 , builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
				.AddPolicy(PolicyNames.CreatedAtleast2Restaurants, builder => builder.AddRequirements(new CreatedMultipleRestaurantsRequirement(2)));

			services.AddScoped<IAuthorizationHandler , MinimumAgeRequirementHandler>();
			services.AddScoped<IAuthorizationHandler , CreatedMultipleRestaurantsRequirementHandler>();
			services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
		}
	}
}
