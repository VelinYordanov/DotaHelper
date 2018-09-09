using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Providers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Tests.Providers.HeroesProviderTests
{
    [TestFixture]
    public class GetAllHeroesShould
    {
        [Test]
        public async Task CallTheCorrectUrlToGetTheData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object,mapper.Object);

            var heroes = await heroesProvider.GetAllHeroes();

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }

        [Test]
        public async Task StoreTheHeroDataAndUseItOnSubsequentCalls()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(new List<HeroDto> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);
            var heroes = await heroesProvider.GetAllHeroes();
            var heroes2 = await heroesProvider.GetAllHeroes();
            var heroes3 = await heroesProvider.GetAllHeroes();

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }

        [Test]
        public async Task ReturnCorrectHeroData()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };
            var list = new List<HeroDto> { hero };           
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(list);
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);

            var heroes = await heroesProvider.GetAllHeroes();

            Assert.AreEqual(1, heroes.Count);
            Assert.IsTrue(heroes.ContainsKey("1"));
            Assert.AreEqual(heroes["1"], hero);
        }
    }
}
