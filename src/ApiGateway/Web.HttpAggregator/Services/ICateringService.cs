using Web.HttpAggregator.Models;

namespace Web.HttpAggregator.Services
{
    public interface ICateringService
    {
        Task<RestaurantData> GetRestaurantByIdAsync(int id);
    }
}
