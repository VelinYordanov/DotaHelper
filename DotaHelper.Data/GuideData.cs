using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data
{
    public class GuideData : DotaHelperGuideRepository, IGuideData
    {
        public GuideData(DotaHelperDbContext dbContext) :base(dbContext)
        {
        }

        public async Task<IEnumerable<Guide>> GetPagedGuidesAsync(int skip, int take)
        {
            return await this.All.Skip(skip).Take(take).ToListAsync();
        }
    }
}
