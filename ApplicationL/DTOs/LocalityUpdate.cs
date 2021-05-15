using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.DTOs
{
    public class LocalityUpdate
    {

        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string CityName { get; set; }
        [MaxLength(30)]
        public string RegionName { get; set; }
    }
}
