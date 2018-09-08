using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Commons.Interfaces
{
    public interface IMapper
    {
        TDest Map<TDest>(object source);
    }
}
