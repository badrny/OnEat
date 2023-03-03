using Microsoft.AspNetCore.Mvc;
using Web.HttpAggregator.Models;
using Web.HttpAggregator.Services;

namespace Web.HttpAggregator.Controllers;


[ApiVersion("1.0")]
[Route(Constants.Routes.RestaurantBaseRoute)]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly ILogger<RestaurantController> _logger;
    private readonly ICateringService _cateringService;
    public RestaurantController(ICateringService cateringService, ILogger<RestaurantController> logger)
    {
        _cateringService = cateringService ?? throw new ArgumentNullException();
        _logger = logger ?? throw new ArgumentNullException();
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantData), StatusCodes.Status200OK)]
    [ActionName(nameof(GetRestaurantByIdAsync))]
    public async Task<IActionResult> GetRestaurantByIdAsync(int id) =>
        id <= 0 ? BadRequest("Id is wrong : try an id higher than zero") :
        Ok(await _cateringService.GetRestaurantByIdAsync(id));
}
