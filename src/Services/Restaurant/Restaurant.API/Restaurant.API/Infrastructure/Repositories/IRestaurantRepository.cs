using Catering.API.Model;

namespace Catering.API.Infrastructure.Repositories;

public interface IRestaurantRepository
{
    Task<Restaurant> GetRestaurantByIdAsync(int id);
    Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
    Task AddRestaurantAsync(Restaurant restaurant);
    Task DeleteRestaurantAsync(Restaurant restaurant);
}
