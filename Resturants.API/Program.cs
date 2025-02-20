using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Infrastructure.Extensions;
using Restaurant.Infrastructure.Seeders;
using Resturants.API.Extensions;
using Resturants.API.Middlewares;
using Serilog;
using Restaurant.Domain.Entites;
using Restaurant.Application.Extenstions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructre(builder.Configuration);


    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

    await seeder.Seed();
    // Configure the HTTP request pipeline.
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.MapGroup("api/identity")
        .WithTags("Identity")
        .MapIdentityApi<User>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}


public partial class Program { }