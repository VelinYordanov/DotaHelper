using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
