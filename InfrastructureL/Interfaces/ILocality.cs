using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureL.Interfaces
{
    public interface ILocality
    {
        bool SaveChanges();
        IEnumerable<Locality> GetAllLocalities();
        Task<Locality> SearchLocalityByName(string name);
        Task<IEnumerable<Locality>> QueryLocalityName(string name);
        void AddNewLocality(Locality locality);
        void UpdateLocality(Locality locality);
        void DeleteLocality(Locality locality);
        Task<Locality> SearchPostCode(string postcode);
    }
}
