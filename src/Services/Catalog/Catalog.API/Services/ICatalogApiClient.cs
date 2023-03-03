namespace Catalog.API.Services
{
    public interface ICatalogApiClient
    {
        Task<object> GetCatalogItemsByNameAsync(string name);
    }
}
