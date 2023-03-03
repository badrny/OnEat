using Catering.API.Infrastructure.Repositories;
using Catering.API.Model;
using Grpc.Core;

namespace GrpcCatering;

public class CateringService : Catering.CateringBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<CateringService> _logger;
    public CateringService(IRestaurantRepository restaurantRepository, ILogger<CateringService> logger)
    {
        _restaurantRepository = restaurantRepository;
        _logger = logger;
    }

    public override async Task<RestaurantResponse> GetRestaurantById(RestaurantRequest request, ServerCallContext context)
    {
        _logger.LogInformation("start gRPC Call from {Metohd} for restaurant id{Id}", context.Method, request.Id);
        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant is not null)
        {
            context.Status = new Status(StatusCode.OK, $"Restaurant with id {request.Id} exist");
            return MapToRestaurantResponse(restaurant);
        }
        else
        {
            context.Status = new Status(StatusCode.NotFound, $"Restaurant with id {request.Id} not exist");
        }
        return new();
    }

    private RestaurantResponse MapToRestaurantResponse(Restaurant restaurant)
    {
        return new()
        {
            Id = restaurant.Id ?? 0,
            Catalogid = restaurant.CatalogId,
            Description = restaurant.Description,
            Name = restaurant.Name
        };

    }
}
