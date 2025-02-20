using Restaurant.Domain.Contants;
using Restaurant.Domain.Entites;
namespace Restaurant.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurants restaurants, ResourceOperation resourceOperation);
    }
}