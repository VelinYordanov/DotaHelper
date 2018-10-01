using DotaHelper.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Commons.Interfaces
{
    public interface IUserProvider
    {
        Task<IdentityUser> GetCurrentUserAsync(HttpContext httpContext);
    }
}
