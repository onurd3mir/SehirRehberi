using AutoMapper;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt => 
                {
                    opt.MapFrom(src => src.photos.FirstOrDefault(p => p.IsMain).Url);
                });

            CreateMap<City, CityForDetailDto>();
            CreateMap<PhotoForCreationDto,Photo>();
            CreateMap<PhotoForReturnDto, Photo>();

        }

    }
}
