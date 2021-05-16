using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainL.Models
{
    public class Locality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConstID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required]
        [MaxLength(30)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(30)]
        public string RegionName { get; set; }

        [Required]
        [MaxLength(2)]
        public string LocalityCode { get; set; }
        public override string ToString()
        {
            return $"{Region.RegionCode} {City.CityCode}{LocalityCode}".ToString();
        }

        //Navigation Properties
        [DisplayName("RegionCode")]
        public virtual Region Region { get; set; }

        [Required]
        [DisplayName("CityCode")]
        public virtual City City { get; set; }
    }
}
