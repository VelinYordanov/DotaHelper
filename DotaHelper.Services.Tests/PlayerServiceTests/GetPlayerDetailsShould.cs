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
    public class GetPlayerDetailsShould
    {
        [Test]
        public async Task GetPlayerWinsAndLossesData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<ICollection<PlayerHeroesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerHeroesDto>());
            mapper.Setup(x => x.Map<ICollection<PlayerRecentMatchesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerRecentMatchesDto>());
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);

            await playerService.GetPlayerDetailsAsync("id");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.PlayerWinLossUrlTemplate, "id"), null), Times.Once);
        }

        [Test]
        public async Task GetPlayerHeroesData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<ICollection<PlayerHeroesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerHeroesDto>());
            mapper.Setup(x => x.Map<ICollection<PlayerRecentMatchesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerRecentMatchesDto>());
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);

            await playerService.GetPlayerDetailsAsync("id");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.PlayerHeroesUrlTemplate, "id"), null), Times.Once);
        }

        [Test]
        public async Task GetPlayerRecentMatchHistoryData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<ICollection<PlayerHeroesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerHeroesDto>());
            mapper.Setup(x => x.Map<ICollection<PlayerRecentMatchesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerRecentMatchesDto>());
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);

            await playerService.GetPlayerDetailsAsync("id");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.PlayerRecentMatchesUrlTemplate, "id"), null), Times.Once);
        }

        [Test]
        public async Task GetPlayerDetailsData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<ICollection<PlayerHeroesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerHeroesDto>());
            mapper.Setup(x => x.Map<ICollection<PlayerRecentMatchesDto>>(It.IsAny<object>())).Returns(() => new List<PlayerRecentMatchesDto>());
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);

            await playerService.GetPlayerDetailsAsync("id");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.PlayerDetailsUrlTemplate, "id"), null), Times.Once);
        }

        [Test]
        public async Task ReturnProperData()
        {
            var heroes = new List<PlayerHeroesDto>();
            var recentMatches = new List<PlayerRecentMatchesDto>();
            var playerProfile = new PlayerProfileDetailsDto();
            var playerWinsAndLosses = new PlayerWinsLossesDto();
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<ICollection<PlayerHeroesDto>>(It.IsAny<object>())).Returns(() => heroes);
            mapper.Setup(x => x.Map<ICollection<PlayerRecentMatchesDto>>(It.IsAny<object>())).Returns(() => recentMatches);
            mapper.Setup(x => x.Map<PlayerProfileDetailsDto>(It.IsAny<object>())).Returns(() => playerProfile);
            mapper.Setup(x => x.Map<PlayerWinsLossesDto>(It.IsAny<object>())).Returns(() => playerWinsAndLosses);
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);

            var result = await playerService.GetPlayerDetailsAsync("id");
            Assert.AreEqual(heroes, result.Heroes);
            Assert.AreEqual(recentMatches, result.RecentMatchHistory);
            Assert.AreEqual(playerWinsAndLosses, result.WinsAndLosses);
            Assert.AreEqual(playerProfile, result.Details);
        }
    }
}
