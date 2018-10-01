using DotaHelper.Data.Models;
using DotaHelper.Models.PostModels;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Controllers
{
    public class GuidesController : Controller
    {
        private readonly IGuidesService guidesService;
        private readonly IItemsProvider itemsProvider;
        private readonly IHeroesProvider heroesProvider;
        private readonly UserManager<DotaHelperUser> userManager;

        public GuidesController(IGuidesService guidesService, IItemsProvider itemsProvider, IHeroesProvider heroesProvider, UserManager<DotaHelperUser> userManager)
        {
            this.guidesService = guidesService ?? throw new ArgumentException(nameof(guidesService));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
            this.userManager = userManager ?? throw new ArgumentException(nameof(userManager));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await this.guidesService.GetCreateModel();
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuidePostDataModel data)
        {
            var itemsTasks = new List<string> { data.Item1, data.Item2, data.Item3, data.Item4, data.Item5, data.Item6 }.Select(async x => await this.itemsProvider.GetItemById(x)).ToList();
            var items = (await Task.WhenAll(itemsTasks)).ToList();
            foreach (var item in items)
            {
                if (item == null)
                {
                    this.ModelState.AddModelError(nameof(item), "Item not found");
                }
            }

            if (await this.heroesProvider.GetHeroAsync(data.HeroId) == null)
            {
                this.ModelState.AddModelError(nameof(data.HeroId), "Hero not found");
            }


            if (!ModelState.IsValid)
            {
                return this.Redirect("/guides/create");
            }

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            await this.guidesService.AddGuide(user.Id, data);

            return this.RedirectToAction("index", "home");
        }
    }
}
