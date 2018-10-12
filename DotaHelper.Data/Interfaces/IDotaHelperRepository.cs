using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data.Interfaces
{
    public interface IDotaHelperRepository<T> where T : class
    {
        void Add(T item);

        void Remove(T item);

        Task<T> FindAsync(object id);

        IQueryable<T> All { get; }

        Task<int> CountAsync();
    }
}
