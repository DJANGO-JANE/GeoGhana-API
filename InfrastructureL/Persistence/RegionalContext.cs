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
        public DbSet<Region> RegionsGH { get; set; }
        public DbSet<City> CitiesGH { get; set; }
        public DbSet<Locality> LocalitiesGH { get; set; }
    }
}
