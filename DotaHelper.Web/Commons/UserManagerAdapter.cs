using DotaHelper.Data.Models;
using DotaHelper.Services.Commons.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class UserManagerAdapter : IUserProvider
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserManagerAdapter(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        public async Task<IdentityUser> GetCurrentUserAsync(HttpContext context)
        {
            return await this.userManager.GetUserAsync(context.User);
        }
    }
}
