using DotaHelper.Services.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class Mapper : IMapper
    {
        private readonly AutoMapper.IMapper mapper;

        public Mapper(AutoMapper.IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TDest Map<TDest>(object source)
        {
            return this.mapper.Map<TDest>(source);
        }
    }
}
