using DotaHelper.Models.Dto;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using DotaHelper.Web.Commons;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Tests.PlayerServiceTests
{
    [TestFixture]
    public class SearchPlayersShould
    {
        [Test]
        public async Task CallTheCorrectEndpointToGetTheData()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object);
            await playerService.SearchPlayers("test");

            httpClient.Verify(x => x.GetAsync(string.Format(DotaApiEndpoints.SearchUrlTemplate, "test"),null), Times.Once);
        }

        [Test]
        public async Task DeserializeTheJsonRecievedFromTheEndpoint()
        {
            var directory = Path.Combine(Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory))).FullName, "PlayerServiceTests","players-search.json");
            var jsonToReturn = File.ReadAllTextAsync(directory);
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(() => jsonToReturn);
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object,heroesProvider.Object);
            await playerService.SearchPlayers("test");

            jsonSerializer.Verify(x => x.Deserialize<List<PlayerSearchJsonModel>>(jsonToReturn.Result));
        }

        [Test]
        public async Task ReturnProperDataAboutSearchedPlayers()
        {
            var directory = Path.Combine(Directory.GetParent(Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory))).FullName, "PlayerServiceTests", "players-search.json");
            var jsonToReturn = File.ReadAllTextAsync(directory);
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(x => x.GetAsync(It.IsAny<string>(), null)).Returns(() => jsonToReturn);
            IJsonSerializer jsonSerializer = new JsonSerializer();
            var configuration = new AutoMapper.MapperConfiguration(cnf => cnf.AddProfile<MappingProfile>());
            IMapper mapper = new Mapper(new AutoMapper.Mapper(configuration));
            var heroesProvider = new Mock<IHeroesProvider>();
            var playerService = new PlayerService(httpClient.Object, jsonSerializer, mapper,heroesProvider.Object);
            var result = (await playerService.SearchPlayers("test")).ToList();

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("1", result[0].AccountId);
            Assert.AreEqual("2", result[1].AccountId);
            Assert.AreEqual("3", result[2].AccountId);
            Assert.AreEqual("test1", result[0].Name);
            Assert.AreEqual("test2", result[1].Name);
            Assert.AreEqual("test3", result[2].Name);
            Assert.AreEqual("avatar1", result[0].AvatarUrl);
            Assert.AreEqual("avatar2", result[1].AvatarUrl);
            Assert.AreEqual("avatar3", result[2].AvatarUrl);
        }
    }
}
