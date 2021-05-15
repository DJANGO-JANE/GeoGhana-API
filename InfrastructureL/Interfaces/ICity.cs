using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureL.Interfaces
{
    public interface ICity
    {
        bool SaveChanges();
        Task<IEnumerable<Locality>> GetAllLocalities();
        Task<IEnumerable<City>> GetAllCities();
        Task<City> SearchCityByCode(int code);
        Task<IEnumerable<City>> QueryCityName(string name);
        Task<City> FindDuplicate (string name, string? regionName);
        void AddNewCity(City city);
        void UpdateCity(City city);
        void DeleteCity(City city);
    }
}
