using GrpcCatering;
using Web.HttpAggregator.Models;

namespace Web.HttpAggregator.Services;

public class CateringService : ICateringService
{
    private readonly Catering.CateringClient _cateringClient;
    private readonly ILogger<CateringService> _logger;
    public CateringService(Catering.CateringClient cateringClient, ILogger<CateringService> logger)
    {
        _cateringClient = cateringClient;
        _logger = logger;
    }

    public async Task<RestaurantData> GetRestaurantByIdAsync(int id)
    {
        _logger.LogDebug("grpc request restaurant by id = {@id}", id);
        var response = await _cateringClient.GetRestaurantByIdAsync(new() { Id = id });
        _logger.LogDebug("grpc response = {@response}", response);
        return MapToRestaurantData(response);       
    }

    private RestaurantData MapToRestaurantData(RestaurantResponse restaurantResponse)
    {
        return new()
        {
            Id = restaurantResponse.Id,
            CatalogID = restaurantResponse.Catalogid,
            Description = restaurantResponse.Description,
            Name = restaurantResponse.Name
        };
    }
}
