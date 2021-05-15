using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.DTOs
{
    public class CityFull
    {
        public string Name { get; set; }
        public string CityCode { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public IEnumerable<LocalityAsChild> Localities { get; set; }
    }
}
