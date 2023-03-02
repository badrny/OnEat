using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Catering.API.Model;

namespace Catering.API.Infrastructure.EntityConfigurations;

public class RestaurantEntityTypeConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable(nameof(Restaurant));

        builder.HasKey(et => et.Id);

        builder
            .Property(et => et.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder
            .Property(et => et.Description)
            .IsRequired()
            .HasMaxLength(300);


        builder
            .Property(et => et.CatalogId)
            .IsRequired();
    }
}