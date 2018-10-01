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
    public class DotaHelperUserRepository : IDotaHelperRepository<DotaHelperUser>
    {
        private readonly DotaHelperDbContext dbContext;
        private readonly DbSet<DotaHelperUser> users;

        public DotaHelperUserRepository(DotaHelperDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            this.users = this.dbContext.Set<DotaHelperUser>();
        }

        public IQueryable<DotaHelperUser> All => this.users;

        public void Add(DotaHelperUser item)
        {
            this.users.Add(item);
        }

        public async Task<DotaHelperUser> FindAsync(object id)
        {
            return await this.users.FindAsync(id);
        }

        public void Remove(DotaHelperUser item)
        {
            this.users.Remove(item);
        }
    }
}
