using DotaHelper.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Data.Interfaces
{
    public interface IDotaHelperDbContext
    {
        DbSet<Guide> Guides { get; set; }

        DbSet<DotaHelperUser> Users { get; set; }
    }
}
