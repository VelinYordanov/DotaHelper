﻿using DotaHelper.Models.Dto;
using DotaHelper.Models.Enums;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Services
{
    public class PlayerService : IPlayerService
    {
        private const int NumberOfBestHeroesToShow = 5;

        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;
        private readonly IHeroesProvider heroesProvider;

        public PlayerService(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper, IHeroesProvider heroesProvider)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
        }

        public async Task<IEnumerable<PlayerSearchDto>> SearchPlayersAsync(string name)
        {
            var url = string.Format(DotaApiEndpoints.SearchUrlTemplate, name);
            var jsonData = await this.httpClient.GetAsync(url);
            var players = this.jsonSerializer.Deserialize<List<PlayerSearchJsonModel>>(jsonData);
            var playerDtos = this.mapper.Map<IEnumerable<PlayerSearchDto>>(players);
            return playerDtos;
        }

        public async Task<PlayerDetailsDto> GetPlayerDetailsAsync(string accountId)
        {
            var detailsUrl = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, accountId);
            var recentMatchesUrl = string.Format(DotaApiEndpoints.PlayerRecentMatchesUrlTemplate, accountId);
            var heroesUrl = string.Format(DotaApiEndpoints.PlayerHeroesUrlTemplate, accountId);
            var winsLossesUrl = string.Format(DotaApiEndpoints.PlayerWinLossUrlTemplate, accountId);

            var detailsDataTask = this.httpClient.GetAsync(detailsUrl);
            var winsLossesTask = this.httpClient.GetAsync(winsLossesUrl);
            var recentMatchesTask = this.httpClient.GetAsync(recentMatchesUrl);
            var heroesTask = this.httpClient.GetAsync(heroesUrl);
            var allHeroesTask = this.heroesProvider.GetAllHeroesAsync();

            await Task.WhenAll(detailsDataTask, winsLossesTask, recentMatchesTask, heroesTask, allHeroesTask);
            var detailsData = detailsDataTask.Result;
            var winLossData = winsLossesTask.Result;
            var recentMatchesData = recentMatchesTask.Result;
            var heroesData = heroesTask.Result;
            var heroIdsToHeroes = allHeroesTask.Result;

            var detailsJsonModel = this.jsonSerializer.Deserialize<PlayerDetailsJsonModel>(detailsData);
            var winLossJsonModel = this.jsonSerializer.Deserialize<PlayerWinsLossesJsonModel>(winLossData);
            var recentMatchesJsonModel = this.jsonSerializer.Deserialize<ICollection<PlayerRecentMatchesJsonModel>>(recentMatchesData);
            var playerHeroesJsonModel = this.jsonSerializer.Deserialize<ICollection<PlayerHeroesJsonModel>>(heroesData)?.Take(NumberOfBestHeroesToShow).ToList();

            var detailsDto = this.mapper.Map<PlayerProfileDetailsDto>(detailsJsonModel);
            var winLossDto = this.mapper.Map<PlayerWinsLossesDto>(winLossJsonModel);
            var recentMatchesDto = this.mapper.Map<ICollection<PlayerRecentMatchesDto>>(recentMatchesJsonModel);
            var heroesDto = this.mapper.Map<ICollection<PlayerHeroesDto>>(playerHeroesJsonModel);
            foreach (var hero in heroesDto)
            {
                hero.Hero = await this.heroesProvider.GetHeroAsync(hero.HeroId);
            }

            foreach (var match in recentMatchesDto)
            {
                match.Hero = await this.heroesProvider.GetHeroAsync(match.HeroId);
            }

            var playerDetailsDto = new PlayerDetailsDto { Details = detailsDto, Heroes = heroesDto, RecentMatchHistory = recentMatchesDto, WinsAndLosses = winLossDto };
            return playerDetailsDto;
        }
    }
}
