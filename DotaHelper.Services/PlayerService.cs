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
        private const string DireTeam = "Dire";
        private const string RadiantTeam = "Radiant";
        private const int PlayerHeroesMinNumberOfGames = 50;
        private const int NumberOfMostPlayedPlayerHeroesToShow = 5;

        private readonly IHttpClient httpClient;
        private readonly IJsonSerializer jsonSerializer;
        private readonly IMapper mapper;
        private readonly IHeroesProvider heroesProvider;

        public PlayerService(IHttpClient httpClient, IJsonSerializer jsonSerializer, IMapper mapper , IHeroesProvider heroesProvider)
        {
            this.httpClient = httpClient ?? throw new ArgumentException(nameof(httpClient));
            this.jsonSerializer = jsonSerializer ?? throw new ArgumentException(nameof(jsonSerializer));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.heroesProvider = heroesProvider ?? throw new ArgumentException(nameof(heroesProvider));
        }

        public async Task<IEnumerable<PlayerSearchDto>> SearchPlayers(string name)
        {
            var url = string.Format(DotaApiEndpoints.SearchUrlTemplate, name);
            var jsonData = await this.httpClient.GetAsync(url);
            var players = this.jsonSerializer.Deserialize<List<PlayerSearchJsonModel>>(jsonData);
            var playerDtos = this.mapper.Map<IEnumerable<PlayerSearchDto>>(players);
            return playerDtos;
        }

        public async Task<PlayerDetailsDto> GetPlayerDetails(string accountId)
        {
            var detailsUrl = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, accountId);
            var recentMatchesUrl = string.Format(DotaApiEndpoints.PlayerRecentMatchesUrlTemplate, accountId);
            var heroesUrl = string.Format(DotaApiEndpoints.PlayerHeroesUrlTemplate, accountId, PlayerHeroesMinNumberOfGames);
            var winsLossesUrl = string.Format(DotaApiEndpoints.PlayerWinLossUrlTemplate, accountId);

            var detailsDataTask = this.httpClient.GetAsync(detailsUrl);
            var winsLossesTask = this.httpClient.GetAsync(winsLossesUrl);
            var recentMatchesTask = this.httpClient.GetAsync(recentMatchesUrl);
            var heroesTask = this.httpClient.GetAsync(heroesUrl);

            await Task.WhenAll(detailsDataTask, winsLossesTask, recentMatchesTask, heroesTask);
            var detailsData = detailsDataTask.Result;
            var winLossData = winsLossesTask.Result;
            var recentMatchesData = recentMatchesTask.Result;
            var heroesData = heroesTask.Result;

            var detailsModel = this.jsonSerializer.Deserialize<PlayerDetailsJsonModel>(detailsData);
            var winLossModel = this.jsonSerializer.Deserialize<PlayerWinsLossesJsonModel>(winLossData);
            var recentMatchesModel = this.jsonSerializer.Deserialize<ICollection<PlayerRecentMatchesJsonModel>>(recentMatchesData);
            var heroesModel = this.jsonSerializer.Deserialize<ICollection<PlayerHeroesJsonModel>>(heroesData).Take(NumberOfMostPlayedPlayerHeroesToShow).ToList();

            foreach (var item in heroesModel)
            {
                var hero = await this.heroesProvider.GetHero(item.HeroId);
                item.HeroName = hero.Name;
                item.HeroImageUrl = hero.ImageUrl;
                item.GamesLost = item.GamesPlayed - item.GamesWon;
                item.WinRate = Math.Round((item.GamesWon / item.GamesPlayed) * 100);
            }

            foreach (var item in recentMatchesModel)
            {
                var hero = await this.heroesProvider.GetHero(item.HeroId);
                item.HeroName = hero.Name;
                item.HeroImageUrl = hero.ImageUrl;

                //https://wiki.teamfortress.com/wiki/WebAPI/GetMatchHistory#Player_Slot
                item.Team = item.PlayerSlot > 128 ? DireTeam : RadiantTeam;
                item.WonGame = item.RadiantWin ? item.Team == RadiantTeam : item.Team == DireTeam;
                item.LobbyType = (LobbyType)item.Lobby;
                item.GameMode = (GameMode)item.Game;
            }

            var playerDetailsDto = new PlayerDetailsDto { Details = detailsModel, Heroes = heroesModel, RecentMatchHistory = recentMatchesModel, WinsAndLosses = winLossModel };
            return playerDetailsDto;
        }
    }
}
