using Catering.API.Infrastructure.Repositories;
using Catering.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace Catering.API.Controllers;

[ApiVersion("1.0")]
[Route(Constants.Routes.RestaurantBaseRoute)]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly ILogger<RestaurantController> _logger;
    public RestaurantController(IRestaurantRepository restaurantRepository, ILogger<RestaurantController> logger)
    {
        _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException();
        _logger = logger ?? throw new ArgumentNullException();
    }

    [HttpGet]
    [ProducesResponseType(typeof(Restaurant), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRestaurantsAsync() =>
        Ok(await _restaurantRepository.GetRestaurantsAsync());

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddRestaurantAsync(Restaurant restaurant)
    {
        _logger.LogInformation($"Add Restaurant : {restaurant.Name}");
        await _restaurantRepository.AddRestaurantAsync(restaurant);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id = restaurant.Id }, null);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRestaurantByIdAsync(int id) =>
        id <= 0 ? BadRequest("Id is wrong : try an id higher than zero") :
        Ok(await _restaurantRepository.GetRestaurantsAsync());

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IEnumerable<Restaurant>), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteRestaurantAsync(int id)
    {
        if (id <= 0) return BadRequest("Id is wrong : try an id higher than zero");
        var restaurant = await _restaurantRepository.GetRestaurantByIdAsync(id);
        await _restaurantRepository.DeleteRestaurantAsync(restaurant);
        return NoContent();
    }
}
