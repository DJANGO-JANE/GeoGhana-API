using DomainL.Models;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureL.Persistence
{
    public class RegionalContext : DbContext
    {
        public RegionalContext(DbContextOptions<RegionalContext> options) : base(options)
        {
            
            //LazyLoadingEnabled = false;
            // this.Configuration.ProxyCreationEnabled = false;

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // configures one-to-many relationship
        //    modelBuilder.Entity<City>()
        //        .HasMany<Locality>().WithOne(x => x.City).OnDelete(DeleteBehavior.Cascade);

        //}

        public DbSet<Region> RegionsGH { get; set; }
        public DbSet<City> CitiesGH { get; set; }
        public DbSet<Locality> LocalitiesGH { get; set; }

        
    }
}
