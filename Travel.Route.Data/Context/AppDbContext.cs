using Microsoft.EntityFrameworkCore;
using Travel.Route.Domain.Entities;

namespace Travel.Route.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>
            options) : base(options)
        {

        }

        public DbSet<TravelRoute> Routes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
