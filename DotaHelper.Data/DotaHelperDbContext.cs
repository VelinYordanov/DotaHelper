using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Data
{
    public class DotaHelperDbContext : IdentityDbContext<DotaHelperUser>, IDotaHelperDbContext
    {
        public DotaHelperDbContext(DbContextOptions<DotaHelperDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DotaHelperUserGuide>().HasKey(x => new { x.DotaHelperUserId, x.GuideId });

            builder.Entity<DotaHelperUserGuide>()
                .HasOne(x => x.Guide)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.GuideId);

            builder.Entity<DotaHelperUserGuide>()
                .HasOne(x => x.User)
                .WithMany(x => x.FavoritedGuides)
                .HasForeignKey(x => x.DotaHelperUserId);

            base.OnModelCreating(builder);
        }
        public DbSet<Guide> Guides { get; set; }
    }
}
