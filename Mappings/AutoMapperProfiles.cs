using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionCreateRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();

            CreateMap<WalkDto,Walk>().ReverseMap();
            CreateMap<AddWalksCreateDto,Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto,Walk>().ReverseMap();

            CreateMap<DifficultyDto,Difficulty>().ReverseMap();


        }
    }
}