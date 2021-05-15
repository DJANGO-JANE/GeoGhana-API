using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfrastructureL.Interfaces
{
    public interface IRegion
    {
        bool SaveChanges();
       // Region ValidateRegionInput(Type region);
        Task<IEnumerable<Region>> GetAllRegions();
        Task<Region> SearchRegionByCode(string regionCode);
        Task<IEnumerable<Region>> QueryRegionName(string regionName);
        void AddNewRegion(Region region);
        void UpdateRegion(Region region);
        void DeleteRegion(Region region);
        IEnumerable<Locality> GetAllLocalities(Region region);
        Task<IEnumerable<City>> GetAllCities(string region);
    }
}
