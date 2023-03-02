using Catering.API.Model;

namespace Catering.API.Infrastructure
{
    public class CateringContextSeed
    {
        public async Task SeedAsync(CateringContext context, ILogger<CateringContextSeed> logger)
        {
            await Task.Run(async () =>
            {
                if (!context.Restaurants.Any())
                {
                    await context.Restaurants.AddRangeAsync(GetPreconfiguredRestaurant());
                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Restaurant> GetPreconfiguredRestaurant()
        {
            return new List<Restaurant>
        {
            new(){CatalogId = 1 , Description ="traditional italian cuisine", Name ="Pasta Box"},
            new(){CatalogId = 2 , Description ="Chiken Shop based in baker and soho is all about tasty chicken and good vibes", Name ="chicken jungle"},
            new(){CatalogId = 3 , Description ="best seafood restaurant in paris", Name ="Shell fish "},
        };
        }
    }
}
