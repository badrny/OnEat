namespace Catalog.API.Services
{
    public class CatalogApiClient : ICatalogApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogApiClient> _logger;
        public CatalogApiClient(HttpClient httpClient, ILogger<CatalogApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = _logger;
        }

        public async Task<object> GetCatalogItemsByNameAsync(string name)
        {
            //To do
            //connect to themealdb.com and get a list of catalagItems
            throw new NotImplementedException();
        }
    }
}
