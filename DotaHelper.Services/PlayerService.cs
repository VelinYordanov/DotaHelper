using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotaHelper.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;

        public PlayerService(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        public async Task<IEnumerable<PlayerSearchDto>> SearchPlayers(string name)
        {
            var url = string.Format(DotaApiEndpoints.SearchUrlTemplate, name);
            var jsonData = await this.httpClient.GetAsync(url);
            var players = this.jsonSerializer.Deserialize<List<PlayerSearchJsonModel>>(jsonData);
            var playerDtos = this.mapper.Map<IEnumerable<PlayerSearchDto>>(players);
            return playerDtos;
        }
    }
}
