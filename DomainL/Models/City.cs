using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainL.Models
{
    public class City
    {
        [Key]
        [MaxLength(2)]
        public int CityCode { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegionName { get; set; }

        public virtual Region Region { get; set; }
        public ICollection<Locality> Localities { get; set; }
    }
}
