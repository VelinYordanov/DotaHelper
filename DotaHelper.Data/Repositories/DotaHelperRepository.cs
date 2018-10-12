using DotaHelper.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Data.Repositories
{
    public class DotaHelperRepository<T> : IDotaHelperRepository<T> where T : class
    {
        private readonly DotaHelperDbContext dbContext;
        private readonly DbSet<T> entities;

        public DotaHelperRepository(DotaHelperDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            this.entities = dbContext.Set<T>();
        }

        public IQueryable<T> All => this.entities;

        public void Add(T item) => this.entities.Add(item);

        public async Task<T> FindAsync(object id) => await this.entities.FindAsync(id);

        public void Remove(T item) => this.entities.Remove(item);

        public async Task<int> Count() => await this.entities.CountAsync();
    }
}
