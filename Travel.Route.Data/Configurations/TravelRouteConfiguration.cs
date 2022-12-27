using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Travel.Route.Domain.Entities;

namespace Travel.Route.Data.Configurations
{
    internal class TravelRouteConfiguration : IEntityTypeConfiguration<TravelRoute>
    {
        public void Configure(EntityTypeBuilder<TravelRoute> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
               .Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder
                .Property(p => p.CreatedDate);

            builder
                .Property(p => p.Origin)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Destination)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(p => p.NumberOfStops)
                .IsRequired();

            builder
                .Property(p => p.Price)
                .IsRequired();
        }
    }
}