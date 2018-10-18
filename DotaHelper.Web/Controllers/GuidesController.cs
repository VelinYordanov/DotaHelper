using DotaHelper.Data.Models;
using DotaHelper.Models.PostModels;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using DotaHelper.Web.ViewModels;
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
        private readonly IUserProvider userProvider;
        private readonly IMapper mapper;

        public GuidesController(IGuidesService guidesService, IItemsProvider itemsProvider, IHeroesProvider heroesProvider, IMapper mapper, IUserProvider userProvider)
        {
            this.guidesService = guidesService ?? throw new ArgumentException(nameof(guidesService));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.userProvider = userProvider ?? throw new ArgumentException(nameof(userProvider));
        }

        public async Task<IActionResult> Index([FromQuery]int page = 1)
        {
            var maxPageTask = this.guidesService.GetGuidesMaxPageAsync();
            var guidesTask = this.guidesService.GetGuidesAsync(page);

            var maxPage = await maxPageTask;
            var guides = await guidesTask;
            var guidesListViewModel = new GuideListViewModel { MaxPage = maxPage, Guides = guides };
            return this.View(guidesListViewModel);
        }

        public async Task<IActionResult> Details([FromRoute]string id)
        {
            var guide = await this.guidesService.GetGuideDetailsAsync(id);
            var viewModel = this.mapper.Map<GuideDetailsViewModel>(guide);
            return this.View(viewModel);
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
            var allItems = await this.itemsProvider.GetAllItemsAsync();
            var items = new List<string> { data.Item1, data.Item2, data.Item3, data.Item4, data.Item5, data.Item6 }.Select(x => allItems.SingleOrDefault(y => y.ItemId == x)).ToList();
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

            var user = await this.userProvider.GetCurrentUserAsync(this.HttpContext);
            await this.guidesService.AddGuide(user.Id, data);

            return this.RedirectToAction("index", "home");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Favorite(string id)
        {
            var user = await this.userProvider.GetCurrentUserAsync(this.HttpContext);
            await this.guidesService.FavoriteGuide(user.Id, id);
            return this.RedirectToAction("index", "guides");
        }
    }
}
