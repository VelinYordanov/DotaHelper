using AutoMapper;
using DotaHelper.Models;
using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<PlayerSearchJsonModel, PlayerSearchDto>().ReverseMap();
        }
    }
}
