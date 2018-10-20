using DotaHelper.Models.Dto;
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
        private readonly IItemsProvider itemsProvider;

        public PlayerService(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper, IHeroesProvider heroesProvider, IItemsProvider itemsProvider)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
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
            var recentMatchesUrl = string.Format(DotaApiEndpoints.PlayerRecentMatchesUrlTemplate, accountId);
            var heroesUrl = string.Format(DotaApiEndpoints.PlayerHeroesUrlTemplate, accountId);
            var winsLossesUrl = string.Format(DotaApiEndpoints.PlayerWinLossUrlTemplate, accountId);

            var detailsTask = this.GetPlayerProfileAsync(accountId);
            var winsLossesTask = this.httpClient.GetAsync(winsLossesUrl);
            var recentMatchesTask = this.httpClient.GetAsync(recentMatchesUrl);
            var heroesTask = this.httpClient.GetAsync(heroesUrl);

            await Task.WhenAll(detailsTask, winsLossesTask, recentMatchesTask, heroesTask);
            var detailsDto = detailsTask.Result;
            var winLossData = winsLossesTask.Result;
            var recentMatchesData = recentMatchesTask.Result;
            var heroesData = heroesTask.Result;

            var winLossJsonModel = this.jsonSerializer.Deserialize<PlayerWinsLossesJsonModel>(winLossData);
            var recentMatchesJsonModel = this.jsonSerializer.Deserialize<ICollection<PlayerRecentMatchesJsonModel>>(recentMatchesData);
            var playerHeroesJsonModel = this.jsonSerializer.Deserialize<ICollection<PlayerHeroesJsonModel>>(heroesData)?.Take(NumberOfBestHeroesToShow).ToList();

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

        public async Task<MatchDetailsDto> GetMatchDetailsAsync(string matchId)
        {
            var matchData = await this.httpClient.GetAsync(string.Format(DotaApiEndpoints.MatchDetailsUrl, matchId));
            var matchDetailsDto = this.mapper.Map<MatchDetailsDto>(this.jsonSerializer.Deserialize<MatchDetailsJsonModel>(matchData));
            foreach (var pickOrBan in matchDetailsDto.PicksAndBans)
            {
                pickOrBan.Hero = await this.heroesProvider.GetHeroAsync(pickOrBan.HeroId);
            }

            var items = await this.itemsProvider.GetAllItemsAsync();
            foreach (var player in matchDetailsDto.Players)
            {
                player.Hero = await this.heroesProvider.GetHeroAsync(player.HeroId);                
                player.Items = player.Items.Where(x => x.ItemId != "0").Select(x => items.SingleOrDefault(y => y.ItemId == x.ItemId)).ToList();
            }

            return matchDetailsDto;
        }

        public async Task<IEnumerable<HeroesListDto>> GetHeroesListDataAsync()
        {
            var heroes = await this.httpClient.GetAsync(DotaApiEndpoints.HeroesListUrl);
            var heroesJsonModel = this.jsonSerializer.Deserialize<IEnumerable<HeroesListJsonModel>>(heroes);
            var heroesDto = this.mapper.Map<IEnumerable<HeroesListDto>>(heroesJsonModel);
            foreach (var hero in heroesDto)
            {
                hero.Hero = await this.heroesProvider.GetHeroAsync(hero.Id);
            }

            return heroesDto;
        }

        public async Task<HeroDetailsDto> GetHeroDetailsAsync(string id)
        {
            var heroTask = this.heroesProvider.GetHeroAsync(id);
            var rankingsTask = this.httpClient.GetAsync(string.Format(DotaApiEndpoints.HeroesRankingsUrlTemplate, id));
            var playersTask = this.httpClient.GetAsync(string.Format(DotaApiEndpoints.HeroesPlayersUrlTemplate, id));
            var matchupsTask = this.httpClient.GetAsync(string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, id));

            var hero = await heroTask;
            var rankingsResponse = await rankingsTask;
            var playersResponse = await playersTask;
            var matchupsResponse = await matchupsTask;

            var rankingsJsonModel = this.jsonSerializer.Deserialize<HeroRankingsJsonModel>(rankingsResponse);
            var playersJsonModel = this.jsonSerializer.Deserialize<IEnumerable<HeroesPlayersJsonModel>>(playersResponse);
            var matchupsJsonModel = this.jsonSerializer.Deserialize<IEnumerable<HeroMatchupsJsonModel>>(matchupsResponse);

            var rankings = this.mapper.Map<IEnumerable<RankingDto>>(rankingsJsonModel).OrderBy(x => x.Score).Take(10).ToList();
            var players = this.mapper.Map<IEnumerable<HeroesPlayersDto>>(playersJsonModel).OrderByDescending(x => x.GamesPlayed).Take(10).ToList();
            var playerProfiles = await Task.WhenAll(players.Select(async x => await this.GetPlayerProfileAsync(x.AccountId)));
            foreach (var player in players)
            {
                player.PlayerProfile = playerProfiles.SingleOrDefault(x => x.AccountId == player.AccountId);
            }

            var matchups = this.mapper.Map<IEnumerable<HeroMatchupsDto>>(matchupsJsonModel);
            foreach (var matchup in matchups)
            {
                matchup.HeroDto = await this.heroesProvider.GetHeroAsync(matchup.HeroId);
            }

            return new HeroDetailsDto { Hero = hero, Players = players, Matchups = matchups, Rankings = rankings };
        }

        private async Task<PlayerProfileDetailsDto> GetPlayerProfileAsync(string id)
        {
            var detailsUrl = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, id);
            var detailsData = await this.httpClient.GetAsync(detailsUrl);
            var detailsJsonModel = this.jsonSerializer.Deserialize<PlayerDetailsJsonModel>(detailsData);
            return this.mapper.Map<PlayerProfileDetailsDto>(detailsJsonModel);
        }
    }
}
