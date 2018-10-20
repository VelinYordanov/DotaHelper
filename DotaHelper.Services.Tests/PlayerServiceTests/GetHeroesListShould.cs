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
    public class GetHeroesListShould
    {
        [Test]
        public async Task CallTheCorrectUrlToGetHeroesData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            await playerService.GetHeroesListDataAsync();

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.HeroesListUrl, null), Times.Once);
        }

        [Test]
        public async Task CallGetHeroForEveryHero()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroesListDto>>(It.IsAny<object>())).Returns(new List<HeroesListDto> { new HeroesListDto { Id = "1" }, new HeroesListDto { Id = "2" }, new HeroesListDto { Id = "3" } });
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            await playerService.GetHeroesListDataAsync();

            heroesProvider.Verify(x => x.GetHeroAsync("1"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("2"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("3"), Times.Once);
        }

        [Test]
        public async Task DeserializeTheCorrectJsonInTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(Task.FromResult("response"));
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            await playerService.GetHeroesListDataAsync();

            jsonSerializer.Verify(x => x.Deserialize<IEnumerable<HeroesListJsonModel>>("response"));
        }

        [Test]
        public async Task MapTheCorrectDataIntoTheCorrectType()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var heroesList = new List<HeroesListJsonModel> { new HeroesListJsonModel { Name="test"} };
            jsonSerializer.Setup(x => x.Deserialize<IEnumerable<HeroesListJsonModel>>(It.IsAny<string>())).Returns(heroesList);
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object);
            await playerService.GetHeroesListDataAsync();

            mapper.Verify(x => x.Map<IEnumerable<HeroesListDto>>(heroesList));
        }
    }
}
