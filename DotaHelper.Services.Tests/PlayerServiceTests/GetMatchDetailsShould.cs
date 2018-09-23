using DotaHelper.Models.Dto;
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
            var matchDetailsDto = new MatchDetailsDto();
            matchDetailsDto.PicksAndBans = new List<PickOrBanDto>();
            matchDetailsDto.Players = new List<MatchPlayerDto>();
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
    }
}
