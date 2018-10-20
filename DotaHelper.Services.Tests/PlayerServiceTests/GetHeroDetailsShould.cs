using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Tests.PlayerServiceTests
{
    [TestFixture]
    public class GetHeroDetailsShould
    {
        private const string Id = "1";

        [Test]
        public async Task CallTheCorrectEndpointToGetHeroRankingsData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var rankingsUrl = string.Format(DotaApiEndpoints.HeroesRankingsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            httpClient.Verify(x => x.GetAsync(rankingsUrl, null), Times.Once);
        }

        [Test]
        public async Task CallTheCorrectEndpointToGetHeroPlayersData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var playersUrl = string.Format(DotaApiEndpoints.HeroesPlayersUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            httpClient.Verify(x => x.GetAsync(playersUrl, null), Times.Once);
        }

        [Test]
        public async Task CallTheCorrectEndpointToGetHeroMatchupsData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var matchupsUrl = string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            httpClient.Verify(x => x.GetAsync(matchupsUrl, null), Times.Once);
        }

        [Test]
        public async Task GetDetailsForHeroFromHeroesProvider()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var matchupsUrl = string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            heroesProvider.Verify(x => x.GetHeroAsync(Id));
        }

        [Test]
        public async Task DeserializeTheHeroRankingsJsonIntoTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(Task.FromResult("response"));
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var matchupsUrl = string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            jsonSerializer.Verify(x => x.Deserialize<HeroRankingsJsonModel>("response"));
        }

        [Test]
        public async Task DeserializeTheHeroPlayersJsonToTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(Task.FromResult("response"));
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var matchupsUrl = string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            jsonSerializer.Verify(x => x.Deserialize<IEnumerable<HeroesPlayersJsonModel>>("response"));
        }

        [Test]
        public async Task DeserializeTheHeroMatchupsJsonToTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(Task.FromResult("response"));
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var matchupsUrl = string.Format(DotaApiEndpoints.HeroesMatchupsUrlTemplate, Id);

            await playerService.GetHeroDetailsAsync(Id);

            jsonSerializer.Verify(x => x.Deserialize<IEnumerable<HeroMatchupsJsonModel>>("response"));
        }

        [Test]
        public async Task MapTheHeroRankingsToTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            var rankings = new HeroRankingsJsonModel();
            var jsonSerializer = new Mock<IJsonSerializer>();
            jsonSerializer.Setup(x => x.Deserialize<HeroRankingsJsonModel>(It.IsAny<string>())).Returns(rankings);
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetHeroDetailsAsync(Id);

            mapper.Verify(x => x.Map<IEnumerable<RankingDto>>(rankings));
        }

        [Test]
        public async Task MapTheHeroPlayersToTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            var players = new List<HeroesPlayersJsonModel>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            jsonSerializer.Setup(x => x.Deserialize<IEnumerable<HeroesPlayersJsonModel>>(It.IsAny<string>())).Returns(players);
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetHeroDetailsAsync(Id);

            mapper.Verify(x => x.Map<IEnumerable<HeroesPlayersDto>>(players));
        }

        [Test]
        public async Task MapTheHeroMatchupsToTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            var matchups = new List<HeroMatchupsJsonModel>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            jsonSerializer.Setup(x => x.Deserialize<IEnumerable<HeroMatchupsJsonModel>>(It.IsAny<string>())).Returns(matchups);
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetHeroDetailsAsync(Id);

            mapper.Verify(x => x.Map<IEnumerable<HeroMatchupsDto>>(matchups));
        }

        [Test]
        public async Task CallGetHeroForEveryMatchup()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var matchups = new List<HeroMatchupsDto> { new HeroMatchupsDto { HeroId = "2" }, new HeroMatchupsDto { HeroId = "3" }, new HeroMatchupsDto { HeroId = "5" } };
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroMatchupsDto>>(It.IsAny<object>())).Returns(matchups);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetHeroDetailsAsync(Id);

            heroesProvider.Verify(x => x.GetHeroAsync("2"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("3"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("5"), Times.Once);
        }

        [Test]
        public async Task GetAccountInfoForEveryPlayer()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var players = new List<HeroesPlayersDto> { new HeroesPlayersDto { AccountId = "2" }, new HeroesPlayersDto { AccountId = "3" }, new HeroesPlayersDto { AccountId = "5" } };
            var playerProfile = new PlayerProfileDetailsDto();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroesPlayersDto>>(It.IsAny<object>())).Returns(players);
            mapper.Setup(x => x.Map<PlayerProfileDetailsDto>(It.IsAny<object>())).Returns(playerProfile);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            var accountUrl1 = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, "2");
            var accountUrl2 = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, "3");
            var accountUrl3 = string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, "5");

            await playerService.GetHeroDetailsAsync(Id);

            httpClient.Verify(x => x.GetAsync(accountUrl1, null), Times.Once);
            httpClient.Verify(x => x.GetAsync(accountUrl2, null), Times.Once);
            httpClient.Verify(x => x.GetAsync(accountUrl3, null), Times.Once);
        }
    }
}
