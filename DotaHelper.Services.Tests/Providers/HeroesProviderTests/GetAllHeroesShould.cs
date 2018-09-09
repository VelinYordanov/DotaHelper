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
        private const string FullHeroNameStart = "npc_dota_hero_";

        [Test]
        public async Task CallTheCorrectUrlToGetTheDataAsync()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            jsonSerializer.Setup(x => x.Deserialize<ICollection<HeroesJsonModel>>(It.IsAny<string>())).Returns(new List<HeroesJsonModel>());
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object);

            var heroes = await heroesProvider.GetAllHeroes();

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }

        [Test]
        public async Task StoreTheHeroDataAndUseItOnSubsequentCalls()
        {
            var hero = new HeroesJsonModel { FullName = FullHeroNameStart + "name", Id = "1", Name = "name" };
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            jsonSerializer.Setup(x => x.Deserialize<ICollection<HeroesJsonModel>>(It.IsAny<string>())).Returns(new List<HeroesJsonModel> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object);
            var heroes = await heroesProvider.GetAllHeroes();
            var heroes2 = await heroesProvider.GetAllHeroes();
            var heroes3 = await heroesProvider.GetAllHeroes();

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }
    }
}
