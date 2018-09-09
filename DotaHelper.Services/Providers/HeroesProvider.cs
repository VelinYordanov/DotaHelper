using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Providers
{
    public class HeroesProvider : IHeroesProvider
    {
        private const string FullHeroNameStart = "npc_dota_hero_";
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private IDictionary<string, HeroDto> heroIdsToHeroes;

        public HeroesProvider(IHttpClient httpClient, IJsonSerializer jsonSerializer)
        {
            this.httpClient = httpClient;
            this.jsonSerializer = jsonSerializer;
            this.heroIdsToHeroes = new Dictionary<string, HeroDto>();
        }

        public async Task<IDictionary<string, HeroDto>> GetAllHeroes()
        {
            if (this.heroIdsToHeroes.Count == 0)
            {
                await this.Refresh();
            }

            return this.heroIdsToHeroes;
        }

        private async Task Refresh()
        {
            var heroes = await this.httpClient.GetAsync(DotaApiEndpoints.AllHeroesUrl);
            var data = this.jsonSerializer.Deserialize<ICollection<HeroesJsonModel>>(heroes);
            var mappedHeroes = data.Select(x => new HeroDto { Id = x.Id, Name = x.Name, ImageUrl = string.Format(DotaApiEndpoints.HeroImageUrlTemplate, x.FullName.Remove(0, FullHeroNameStart.Length)) });
            this.heroIdsToHeroes = mappedHeroes.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());
        }

        public async Task<HeroDto> GetHero(string id)
        {
            if (!this.heroIdsToHeroes.ContainsKey(id))
            {
                await Refresh();
            }

            HeroDto hero = null;
            heroIdsToHeroes.TryGetValue(id, out hero);
            return hero;
        }
    }
}
