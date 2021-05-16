using ApplicationL.DTOs;
using AutoMapper;
using DomainL.Models;
using InfrastructureL.Interfaces;
using InfrastructureL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.Services
{
    public class RegionService : IRegion
    {
        private readonly RegionalContext _regionalContext;

        public RegionService(RegionalContext context)
        {
            _regionalContext = context;
        }

        public void AddNewRegion(Region region)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }
            region.RegionCode = region.RegionCode.ToUpper();
            region.RegionCode += "X";
            _regionalContext.RegionsGH.Add(region);
        }

        public void DeleteRegion(Region region)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetAllCities(string region)
        {
           return (IEnumerable<City>)await _regionalContext.CitiesGH.FindAsync(
                                                    _regionalContext.RegionsGH
                                                    .Include(region => region.RegionCode)
                                                    .ToListAsync());
        }

        public IEnumerable<Locality> GetAllLocalities(Region region)
        {
            return _regionalContext.LocalitiesGH.ToList();
        }

        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            var results = await _regionalContext.RegionsGH
                         .Include(x => x.Cities)
                         .Include(x => x.Localities)
                         .ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Region>> QueryRegionName(string regionName)
        {
            var result = await _regionalContext.RegionsGH
                                        .Include(x => x.Cities)
                                        .Include(x => x.Localities)
                                        .Where(x => x.Name.Contains(regionName)).ToListAsync();
           return result;
        }

        public bool SaveChanges()
        {
            return (_regionalContext.SaveChanges() >= 0);
        }

        public async Task<Region> SearchRegionByCode(string regionCode)
        {
            var result = await _regionalContext.RegionsGH
                            .Include(p => p.Cities)
                                 .Where(p => p.RegionCode == regionCode)
                            .Include(r => r.Localities)
                            .FirstOrDefaultAsync();

            return result;
        }



        public void UpdateRegion(Region region)
        {
        }
    }
}
