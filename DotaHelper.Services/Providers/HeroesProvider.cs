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
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;
        private IDictionary<string, HeroDto> heroIdsToHeroes;

        public HeroesProvider(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
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
            var data = this.jsonSerializer.Deserialize<ICollection<HeroJsonModel>>(heroes);
            var mappedHeroes = mapper.Map<IEnumerable<HeroDto>>(data);
            this.heroIdsToHeroes = mappedHeroes.GroupBy(x => x.Id).ToDictionary(x => x.Key, x => x.First());
        }

        public async Task<HeroDto> GetHero(string id)
        {
            if (!this.heroIdsToHeroes.ContainsKey(id))
            {
                await Refresh();
            }

            heroIdsToHeroes.TryGetValue(id, out HeroDto hero);
            return hero;
        }
    }
}
