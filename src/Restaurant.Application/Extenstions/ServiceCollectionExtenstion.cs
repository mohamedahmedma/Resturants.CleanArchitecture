using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Restaurant;
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using MediatR;
using Restaurant.Application.Users;

namespace Restaurant.Application.Extenstions
{
	public static class ServiceCollectionExtenstion
	{
		public static void AddApplication(this IServiceCollection services)
		{
			var appAssembley = typeof(ServiceCollectionExtenstion).Assembly;

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(appAssembley));

			services.AddAutoMapper(appAssembley);

			services.AddValidatorsFromAssembly(appAssembley)
				.AddFluentValidationAutoValidation();

			services.AddScoped<IUserContext, UserContext>();

			services.AddHttpContextAccessor();
		}
	}
}
