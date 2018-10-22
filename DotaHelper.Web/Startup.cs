using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Web.Commons;
using DotaHelper.Services.Interfaces;
using DotaHelper.Services;
using DotaHelper.Services.Providers;
using DotaHelper.Data.Models;
using DotaHelper.Data;
using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Repositories;

namespace DotaHelper.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Services.Commons.Interfaces.IMapper, Commons.Mapper>();
            services.AddSingleton<IHttpClient, HttpClientAdapter>();
            services.AddSingleton<IJsonSerializer, JsonSerializer>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IDotaHelperData, DotaHelperData>();
            services.AddScoped<IDotaHelperRepository<DotaHelperUser>, DotaHelperRepository<DotaHelperUser>>();
            services.AddScoped<IDotaHelperRepository<Guide>, DotaHelperRepository<Guide>>();
            services.AddScoped<IDotaHelperRepository<DotaHelperUserGuide>, DotaHelperRepository<DotaHelperUserGuide>>();
            services.AddScoped<IGuideData, GuideData>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGuidesService, GuidesService>();
            services.AddScoped<IHeroesProvider, HeroesProvider>();
            services.AddScoped<IItemsProvider, ItemsProvider>();
            services.AddScoped<IUserProvider, UserManagerAdapter>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<DotaHelperDbContext>(options =>
                options.UseLazyLoadingProxies()
                .UseSqlServer(
                    Configuration.GetConnectionString("DotaHelperDbConnection")));
            services.AddDefaultIdentity<DotaHelperUser>()
                .AddEntityFrameworkStores<DotaHelperDbContext>();

            services.Configure<IdentityOptions>(ops =>
            {
                ops.Password.RequireDigit = false;
                ops.Password.RequireUppercase = false;
                ops.Password.RequireDigit = false;
                ops.Password.RequireNonAlphanumeric = false;
            });

            services.AddMemoryCache();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "heroes",
                    template: "heroes",
                    defaults: new { controller = "Players", action = "Heroes" });                    

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
