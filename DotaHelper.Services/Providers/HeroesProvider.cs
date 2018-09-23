using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Providers
{
    public class HeroesProvider : IHeroesProvider
    {
        private const string HeroesCachingKey = "heroes";

        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public HeroesProvider(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper, IMemoryCache cache)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.cache = cache ?? throw new ArgumentException(nameof(cache));
        }

        public async Task<IEnumerable<HeroDto>> GetAllHeroesAsync()
        {
            if (!this.cache.TryGetValue(HeroesCachingKey, out List<HeroDto> heroes))
            {
                await this.Refresh();
            }

            this.cache.TryGetValue(HeroesCachingKey, out heroes);
            return heroes;
        }

        private async Task Refresh()
        {
            var heroes = await this.httpClient.GetAsync(DotaApiEndpoints.AllHeroesUrl);
            this.cache.Set(HeroesCachingKey, this.mapper.Map<IEnumerable<HeroDto>>(this.jsonSerializer.Deserialize<ICollection<HeroJsonModel>>(heroes)));
        }

        public async Task<HeroDto> GetHeroAsync(string id)
        {
            var hero = (await this.GetAllHeroesAsync()).SingleOrDefault(x => x.Id == id);

            if (hero == null)
            {
                await Refresh();
            }
            else
            {
                return hero;
            }

            return (await this.GetAllHeroesAsync()).SingleOrDefault(x => x.Id == id);
        }
    }
}
