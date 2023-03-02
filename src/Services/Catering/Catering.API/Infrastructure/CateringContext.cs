using Microsoft.EntityFrameworkCore;
using Catering.API.Model;
using Catering.API.Infrastructure.EntityConfigurations;

namespace Catering.API.Infrastructure
{
    public class CateringContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

        public CateringContext(DbContextOptions<CateringContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RestaurantEntityTypeConfiguration());
        }
    }
}
