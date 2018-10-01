using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data.Repositories
{
    public class DotaHelperGuideRepository : IDotaHelperRepository<Guide>
    {
        private readonly DotaHelperDbContext dbContext;
        private readonly DbSet<Guide> guides;

        public DotaHelperGuideRepository(DotaHelperDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            this.guides = this.dbContext.Set<Guide>();
        }

        public IQueryable<Guide> All => this.guides;

        public void Add(Guide item)
        {
            this.guides.Add(item);
        }

        public async Task<Guide> FindAsync(object id)
        {
            return await this.guides.FindAsync(id);
        }

        public void Remove(Guide item)
        {
            this.guides.Remove(item);
        }
    }
}
