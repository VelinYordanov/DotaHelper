using DotaHelper.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data.Interfaces
{
    public interface IGuideData
    {
        Task<IEnumerable<Guide>> GetPagedGuidesAsync(int skip, int take);
    }
}
