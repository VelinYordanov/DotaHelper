﻿using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Models.Dto;
using DotaHelper.Models.PostModels;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Services
{
    public class GuidesService : IGuidesService
    {
        const int ElementsPerPage = 5;

        private readonly IDotaHelperData dotaHelperData;
        private readonly IHeroesProvider heroesProvider;
        private readonly IItemsProvider itemsProvider;
        private readonly IMapper mapper;

        public GuidesService(IDotaHelperData dotaHelperData, IHeroesProvider heroesProvider, IItemsProvider itemsProvider, IMapper mapper)
        {
            this.dotaHelperData = dotaHelperData ?? throw new ArgumentException(nameof(dotaHelperData));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        public async Task<IEnumerable<GuideListDto>> GetGuidesAsync(int page = 1)
        {
            var maxPage = await this.GetGuidesMaxPageAsync();
            if (page > maxPage)
            {
                page = maxPage;
            }

            if (page < 1)
            {
                page = 1;
            }

            var skip = (page - 1) * ElementsPerPage;
            var take = ElementsPerPage;
            var guides = await this.dotaHelperData.Guides.GetPagedGuidesAsync(skip, take);
            var guidesList = this.mapper.Map<IEnumerable<GuideListDto>>(guides);
            foreach (var item in guidesList)
            {
                item.HeroImageUrl = (await this.heroesProvider.GetHeroAsync(item.HeroId)).ImageUrl;
            }

            return guidesList;
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

        public async Task FavoriteGuide(string userId, string guideId)
        {
            var user = await this.dotaHelperData.Users.FindAsync(userId);
            var guide = await this.dotaHelperData.Guides.FindAsync(Guid.Parse(guideId));
            var userGuide = new DotaHelperUserGuide { DotaHelperUserId = user.Id, GuideId = guide.Id, Guide = guide, User = user };

            if (user.FavoritedGuides.Select(x => x.Guide).Contains(guide))
            {
                //user.FavoritedGuides.Remove(guide);
            }
            else
            {
                this.dotaHelperData.UserGuides.Add(userGuide);
            }

            await this.dotaHelperData.SaveChangesAsync();
        }

        public async Task<int> GetGuidesMaxPageAsync()
        {
            return (int)Math.Ceiling((await this.dotaHelperData.Guides.CountAsync()) / (double)ElementsPerPage);
        }
    }
}
