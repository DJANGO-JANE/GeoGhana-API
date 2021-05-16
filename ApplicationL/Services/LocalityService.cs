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
    public class LocalityService : ILocality
    {
        private readonly RegionalContext _regionalContext;

        public LocalityService(RegionalContext context)
        {
            _regionalContext = context;
        }
        public void AddNewLocality(Locality locality)
        {
            if (locality == null)
            {
                throw new ArgumentNullException(nameof(locality));
            }

            Region region = _regionalContext.RegionsGH.Single(r => r.Name == locality.RegionName);
            City city = _regionalContext.CitiesGH.Single(r => r.Name == locality.CityName & r.RegionName == locality.RegionName);
            Locality cc = new Locality
            {
                Name = locality.Name,
                RegionName = locality.RegionName,
                Region = region,
                CityName = locality.CityName,
                City = city,
                LocalityCode = locality.LocalityCode.ToUpper()
                
            };

            _regionalContext.LocalitiesGH.Add(cc);

        }

        public void DeleteLocality(Locality locality)
        {
            _regionalContext.LocalitiesGH.Remove(locality);
        }

        public async Task<IEnumerable<Locality>> QueryLocalityName(string name)
        {
            var request = await _regionalContext.LocalitiesGH
                                               .Include(x => x.Region)
                                               .Include(x => x.City)
                                               .Where(x => x.Name.Contains(name)).ToListAsync();

            return request;
        }


        public IEnumerable<Locality> GetAllLocalities()
        {
            return _regionalContext.LocalitiesGH
                                .Include(x => x.Region)
                                .Include(x => x.City)
                                .ToList();
        }

        public bool SaveChanges()
        {
            return (_regionalContext.SaveChanges() >= 0);
        }

        public async Task<Locality> SearchLocalityByName(string code)
        {
            var request = await _regionalContext.LocalitiesGH
                                    .Include(x => x.Region)
                                    .Include(y => y.City)
                                    .FirstOrDefaultAsync(x => x.LocalityCode == code);
            return request;
        }

        public void UpdateLocality(Locality locality)
        {
        }

        public async Task<Locality> SearchPostCode(string postcode)
        {
            postcode=postcode.ToUpper();
            if(postcode.Length==7)
            {
                postcode=postcode.Insert(3, " ");
            }
            if (postcode.Length > 8 || postcode.Length < 7)
            {
                throw new ApplicationException("Invalid Postcode.\nPostcode must be 7-8 characters.");
            }
            if (postcode.Length == 8 && postcode.ElementAt(3)!=' ')
            {
                throw new ApplicationException("4th character of 8-digit postcode must be a whitespace.");
            }
            var results = await _regionalContext.LocalitiesGH
                                            .Include(x => x.Region)
                                            .Include(x => x.City)
                                            .ToListAsync();
            Locality temp = new Locality();
            foreach (var item in results)
            {
                if (postcode == item.ToString())
                {
                    temp = item;
                }
            }
            return temp;
        }
    }
}
