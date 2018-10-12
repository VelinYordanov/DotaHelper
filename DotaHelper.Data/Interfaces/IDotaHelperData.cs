using DotaHelper.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data.Interfaces
{
    public interface IDotaHelperData
    {
        IDotaHelperRepository<DotaHelperUser> Users { get; }

        IGuideData Guides { get; }

        IDotaHelperRepository<DotaHelperUserGuide> UserGuides { get; }

        Task SaveChangesAsync();
    }
}
