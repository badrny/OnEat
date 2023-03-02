using Catering.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Catering.API.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly CateringContext _restaurantContext;
    public RestaurantRepository(CateringContext context)
    {
        _restaurantContext = context;
    }

    public async Task AddRestaurantAsync(Restaurant restaurant)
    {
        await _restaurantContext.Restaurants.AddAsync(restaurant);
        await _restaurantContext.SaveChangesAsync();
    }

    public async Task<Restaurant> GetRestaurantByIdAsync(int id) =>
        await _restaurantContext.
        Restaurants.
        Where(re => re.Id == id).
        SingleOrDefaultAsync() ?? new();

    public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync() =>
        await _restaurantContext.
        Restaurants.
        ToListAsync();

    public async Task DeleteRestaurantAsync(Restaurant restaurant)
    {
        _restaurantContext.Remove(restaurant);
        await _restaurantContext.SaveChangesAsync();
    }
}
