using DotaHelper.Models.Dto;
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
    public class GetHeroShould
    {
        [Test]
        public async Task GetAllHeroesIfIdIsMissing()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(new List<HeroDto> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);

            await heroesProvider.GetHero("1");

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }

        [Test]
        public async Task NotGetAllHeroesDataIfHeroInformationIsAvailable()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(new List<HeroDto> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);

            await heroesProvider.GetAllHeroes();
            await heroesProvider.GetHero("1");

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Once);
        }

        [Test]
        public async Task GetAllHeroesDataAgainIfHeroIdIsNotPresent()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(new List<HeroDto> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);

            await heroesProvider.GetAllHeroes();
            await heroesProvider.GetHero("2");

            httpClient.Verify(x => x.GetAsync(DotaApiEndpoints.AllHeroesUrl, null), Times.Exactly(2));
        }

        [Test]
        public async Task ReturnTheCorrectInformationAboutTheHero()
        {
            var hero = new HeroDto { ImageUrl = "name", Id = "1", Name = "name" };

            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<IEnumerable<HeroDto>>(It.IsAny<object>())).Returns(new List<HeroDto> { hero });
            var heroesProvider = new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper.Object);

            var recievedHero = await heroesProvider.GetHero("1");

            Assert.AreEqual(hero, recievedHero);
        }
    }
}
