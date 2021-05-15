using AutoMapper;
using DomainL.Models;
using InfrastructureL.Interfaces;
using InfrastructureL.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationL.Services
{
    public class CityService : ICity
    {
        private readonly RegionalContext _regionalContext;
        private readonly IMapper _mapper;

        public CityService(RegionalContext context, IMapper mapper)
        {
            _regionalContext = context;
            _mapper = mapper;
        }

        public void AddNewCity(City city)
        {
            if (city == null)
            {
                throw new ArgumentNullException(nameof(city));
            }
            //This just works. Tamper at your own risk
            Region region = _regionalContext.RegionsGH.Single(r => r.Name == city.RegionName);
            City cityTemp = new City
            {
                Name = city.Name,
                RegionName = city.RegionName,
                Region = region,
                CityCode = city.CityCode
            };
            _regionalContext.CitiesGH.Add(cityTemp);
        }

        public void DeleteCity(City city)
        {
            _regionalContext.CitiesGH.Remove(city);
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            return await _regionalContext.CitiesGH.ToListAsync();
        }

        public async Task<IEnumerable<Locality>> GetAllLocalities()
        {
            return await _regionalContext.LocalitiesGH.ToListAsync();
        }

        public bool SaveChanges()
        {
            return (_regionalContext.SaveChanges() >= 0);
        }

        public async Task<City> SearchCityByCode(int code)
        {
            var request = await _regionalContext.CitiesGH
                                        .Include(x => x.Region)
                                        .Include(x => x.Localities)
                                        .FirstOrDefaultAsync(p => p.CityCode == code);
            return request;
        }

        public async Task<City> FindDuplicate(string name, string ?regionName)
        {
            var result = await _regionalContext.CitiesGH
                                                .Include(x => x.Region)
                                                .SingleOrDefaultAsync(x => x.Name == name & x.Region.Name == regionName);
            return result;
        }

        public void UpdateCity(City city)
        {
        }

        public async Task<IEnumerable<City>> QueryCityName(string name)
        {
            var result = await _regionalContext.CitiesGH
                                         .Where(x => x.Name.Contains(name)).ToListAsync();
            return result;
        }
    }
}
