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
    public class GetMatchDetailsShould
    {
        [Test]
        public async Task CallTheCorrectEndPointToGetTheData()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto>(),
                Players = new List<MatchPlayerDto>()
            };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.MatchDetailsUrl, "id"), null), Times.Once);
        }

        [Test]
        public async Task DeserializeTheJsonIntoTheCorrectType()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto>(),
                Players = new List<MatchPlayerDto>()
            };

            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(Task.FromResult("response"));
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            jsonSerializer.Verify(x => x.Deserialize<MatchDetailsJsonModel>("response"));
        }

        [Test]
        public async Task MapTheMatchDetailsToTheCorrectType()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto>(),
                Players = new List<MatchPlayerDto>()
            };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var matchDetails = new MatchDetailsJsonModel();
            jsonSerializer.Setup(x => x.Deserialize<MatchDetailsJsonModel>(It.IsAny<string>())).Returns(matchDetails);
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            mapper.Verify(x => x.Map<MatchDetailsDto>(matchDetails), Times.Once);
        }

        [Test]
        public async Task CallGetHeroForEveryPickOrBan()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto> { new PickOrBanDto { HeroId="1"}, new PickOrBanDto { HeroId = "3" }, new PickOrBanDto { HeroId = "5" } },
                Players = new List<MatchPlayerDto>()
            };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            heroesProvider.Verify(x => x.GetHeroAsync("1"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("3"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("5"), Times.Once);
        }

        [Test]
        public async Task CallGetHeroForEveryPlayer()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto>(),
                Players = new List<MatchPlayerDto> { new MatchPlayerDto { HeroId = "11", Items =  new List<ItemDto>()}, new MatchPlayerDto { HeroId = "33", Items = new List<ItemDto>() }, new MatchPlayerDto { HeroId = "55", Items = new List<ItemDto>() } }
            };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            IEnumerable<ItemDto> items = new List<ItemDto>();
            itemsProvider.Setup(x => x.GetAllItemsAsync()).Returns(Task.FromResult(items));
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            heroesProvider.Verify(x => x.GetHeroAsync("11"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("33"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("55"), Times.Once);
        }

        [Test]
        public async Task CallGetAllItemsInOrderToSetPlayerItems()
        {
            var matchDetailsDto = new MatchDetailsDto
            {
                PicksAndBans = new List<PickOrBanDto>(),
                Players = new List<MatchPlayerDto> { new MatchPlayerDto { HeroId = "11", Items = new List<ItemDto>() }, new MatchPlayerDto { HeroId = "33", Items = new List<ItemDto>() }, new MatchPlayerDto { HeroId = "55", Items = new List<ItemDto>() } }
            };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<MatchDetailsDto>(It.IsAny<object>())).Returns(() => matchDetailsDto);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            IEnumerable<ItemDto> items = new List<ItemDto>();
            itemsProvider.Setup(x => x.GetAllItemsAsync()).Returns(Task.FromResult(items));
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);

            await playerService.GetMatchDetailsAsync("id");

            itemsProvider.Verify(x => x.GetAllItemsAsync(), Times.Once);
        }
    }
}
