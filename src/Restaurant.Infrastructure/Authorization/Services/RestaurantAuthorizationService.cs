using Microsoft.Extensions.Logging;
using Restaurant.Application.Users;
using Restaurant.Domain.Contants;
using Restaurant.Domain.Entites;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurants restaurants,ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user.Email, resourceOperation, restaurants.Name);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }
            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRole.Admin))
            {
                logger.LogInformation("Admin user , delete operation - successful authorization");
                return true;
            }
            if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
                && user.Id == restaurants.OwnerId)
            {
                logger.LogInformation("Restaurant Owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
