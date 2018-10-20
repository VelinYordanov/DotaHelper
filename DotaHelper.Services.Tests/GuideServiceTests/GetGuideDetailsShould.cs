using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Models.Dto;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Tests.GuideServiceTests
{
    [TestFixture]
    public class GetGuideDetailsShould
    {
        [Test]
        public async Task GetTheGuideFromTheDatabase()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<GuideDetailsDto>(It.IsAny<object>())).Returns(new GuideDetailsDto { ItemIds = new List<string>() });

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuideDetailsAsync("1");

            guides.Verify(x => x.FindAsync("1"), Times.Once);
        }

        [Test]
        public async Task MapTheGuideToTheCorrectType()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            var guide = new Guide();
            guides.Setup(x => x.FindAsync("1")).Returns(Task.FromResult(guide));
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<GuideDetailsDto>(It.IsAny<object>())).Returns(new GuideDetailsDto { ItemIds = new List<string>() });

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuideDetailsAsync("1");

            mapper.Verify(x => x.Map<GuideDetailsDto>(guide), Times.Once);
        }

        [Test]
        public async Task GetGameItems()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<GuideDetailsDto>(It.IsAny<object>())).Returns(new GuideDetailsDto { ItemIds = new List<string>() });

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuideDetailsAsync("1");

            itemsProvider.Verify(x => x.GetAllItemsAsync(), Times.Once);
        }

        [Test]
        public async Task GetGuideHero()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<GuideDetailsDto>(It.IsAny<object>())).Returns(new GuideDetailsDto { ItemIds = new List<string>(), HeroId="9" });

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuideDetailsAsync("1");

            heroesProvider.Verify(x => x.GetHeroAsync("9"), Times.Once);
        }
    }
}
