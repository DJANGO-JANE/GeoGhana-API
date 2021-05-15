using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.DTOs
{
    public class RegionFull
    {
        public string RegionCode { get; set; }
        public string Name { get; set; }
        public string CapitalCity { get; set; }
        public IEnumerable<CityAsChild> Cities { get; set; }
        //public IEnumerable<Locality> Localities { get; set; }
    }
}
