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
    public class ItemsProvider : IItemsProvider
    {
        private const string ItemsCacheKey = "items";
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;
        private readonly IMemoryCache cache;

        public ItemsProvider(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper, IMemoryCache cache)
        {
            this.httpClient = httpClient;
            this.jsonSerializer = jsonSerializer;
            this.mapper = mapper;
            this.cache = cache;
        }

        public async Task<IEnumerable<ItemDto>> GetAll()
        {
            if (!this.cache.TryGetValue(ItemsCacheKey, out List<ItemDto> items))
            {
                await this.Refresh();
            }

            this.cache.TryGetValue(ItemsCacheKey, out items);
            return items;
        }

        public async Task<ItemDto> GetItemById(string id)
        {
            var item = (await this.GetAll()).SingleOrDefault(x => x.ItemId == id);
            if (item == null)
            {
                await this.Refresh();
            }
            else
            {
                return item;
            }

            return (await this.GetAll()).SingleOrDefault(x => x.ItemId == id);
        }

        private async Task Refresh()
        {
            string itemData = await this.httpClient.GetAsync(DotaApiEndpoints.AllItemsUrl);
            var items = this.mapper.Map<IEnumerable<ItemDto>>(this.jsonSerializer.Deserialize<IEnumerable<ItemJsonModel>>(itemData));
            foreach (var item in items)
            {
                item.ImageUrl = string.Format(DotaApiEndpoints.ItemImageUrlTemplate, item.Image);
            }

            this.cache.Set(ItemsCacheKey, items);
            //this.cache.Set(ItemsCacheKey, this.mapper.Map<IEnumerable<ItemDto>>(this.jsonSerializer.Deserialize<IEnumerable<ItemJsonModel>>(itemData)));
        }
    }
}
