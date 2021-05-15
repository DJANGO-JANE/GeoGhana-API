using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.DTOs
{
    public class CityUpdate
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string CityCode { get; set; }
        [Required]
        [MaxLength(30)]
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
    }
}
