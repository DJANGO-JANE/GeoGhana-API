using ApplicationL.DTOs;
using AutoMapper;
using DomainL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationL.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityView>()
                                        .ForMember(x => x.CityCode, y => y.MapFrom(z => z.CityCode));
            CreateMap<CityAdd, City>();
            CreateMap<City, CityFull>()
                                        .ForMember(x => x.Localities, y => y.MapFrom(z => z.Localities))
                                        .ForMember(x => x.RegionCode, y => y.MapFrom(z => z.Region.RegionCode));
            CreateMap<City, CityAsChild>()
                                          .ForMember(x => x.Localities, y => y.MapFrom(z => z.Localities));
            CreateMap<City, CityUpdate>();
            CreateMap<CityUpdate, City>();
        }
    }
}
