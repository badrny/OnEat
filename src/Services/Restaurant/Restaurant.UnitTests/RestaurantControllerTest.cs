using Catering.API.Controllers;
using Catering.API.Infrastructure.Repositories;
using Catering.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catering.UnitTests
{
    public class RestaurantControllerTest
    {
        private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock;
        private readonly Mock<ILogger<RestaurantController>> _loggerMock;
        private readonly RestaurantController _restaurantController;
        public RestaurantControllerTest()
        {
            _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            _loggerMock = new Mock<ILogger<RestaurantController>>();
            _restaurantController = new RestaurantController(_restaurantRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Get_restaurant_by_id_success()
        {
            //Arrang
            _restaurantRepositoryMock.Setup(x => x.GetRestaurantByIdAsync(It.IsAny<int>())).ReturnsAsync(new Restaurant());

            //Act
            var actionResult = await _restaurantController.GetRestaurantByIdAsync(1) as OkObjectResult;

            //assert
            Assert.Equal(actionResult?.StatusCode, StatusCodes.Status200OK);
        }
    }
}