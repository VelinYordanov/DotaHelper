using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Models.PostModels;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services
{
    public class GuidesService : IGuidesService
    {
        private readonly IDotaHelperData dotaHelperData;
        private readonly IHeroesProvider heroesProvider;
        private readonly IItemsProvider itemsProvider;

        public GuidesService(IDotaHelperData dotaHelperData, IHeroesProvider heroesProvider, IItemsProvider itemsProvider)
        {
            this.dotaHelperData = dotaHelperData ?? throw new ArgumentException(nameof(dotaHelperData));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
        }

        public async Task AddGuide(string userId, GuidePostDataModel data)
        {
            var user = await this.dotaHelperData.Users.FindAsync(userId);
            var guideToAdd = new Guide { Creator = user, HeroId = data.HeroId, Title = data.Title, Text = data.Text, Item1Id = data.Item1, Item2Id = data.Item2, Item3Id = data.Item3, Item4Id = data.Item4, Item5Id = data.Item5, Item6Id = data.Item6 };
            user.PostedGuides.Add(guideToAdd);
            
            await this.dotaHelperData.SaveChangesAsync();
        }

        public async Task<GuidePostDataModel> GetCreateModel()
        {
            var itemsTask = this.itemsProvider.GetAll();
            var heroesTask = this.heroesProvider.GetAllHeroesAsync();
            var items = await itemsTask;
            var heroes = await heroesTask;

            var heroIdsToNames = heroes.ToDictionary(x => x.Id, x => x.Name);
            var itemIdsToItemNames = items.ToDictionary(x => x.ItemId, x => x.Name);
            var model = new GuidePostDataModel { HeroIdsToNames = heroIdsToNames, ItemIdsToNames = itemIdsToItemNames };
            return model;
        }
    }
}
