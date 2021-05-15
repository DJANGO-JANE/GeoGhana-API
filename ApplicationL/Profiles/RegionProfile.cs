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
    public class RegionProfile : Profile
    {
        public RegionProfile()
        {
            CreateMap<Region, RegionView>()
                                        .ForMember(x =>x.NumberOfCities, y=>y.MapFrom(z=>z.Cities.Count))
                                        .ForMember(x => x.NumberOfLocalities, y => y.MapFrom(z => z.Localities.Count));
            CreateMap<Region, RegionFull>()
                                              .ForMember(x => x.Cities, x => x.MapFrom(z => z.Cities))
                                              ;
            //.ForSourceMember(src=>src.Cities)
            CreateMap<RegionAdd, Region>();
            CreateMap<Region, RegionFull>();
            CreateMap<Region, RegionUpdate>();
            CreateMap<RegionUpdate, Region>();
        }
    }
}
