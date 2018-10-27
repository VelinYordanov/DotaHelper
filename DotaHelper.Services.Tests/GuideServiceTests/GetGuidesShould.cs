using DotaHelper.Data.Interfaces;
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
    public class GetGuidesShould
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task CorrectlySkipAndTakeBasedOnPage(int page)
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.CountAsync()).Returns(Task.FromResult(50));
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuidesAsync(page);
            var skip = (page - 1) * 5;
            var take = 5;

            guides.Verify(x => x.GetPagedGuidesAsync(skip, take));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        [TestCase(-30)]
        public async Task CorrectlySkipAndTakeWhenPageIsNegativeOr0(int page)
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.CountAsync()).Returns(Task.FromResult(50));
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuidesAsync(page);
            var skip = 0;
            var take = 5;

            guides.Verify(x => x.GetPagedGuidesAsync(skip, take));
        }

        [TestCase(100)]
        [TestCase(300)]
        [TestCase(12300)]
        [TestCase(10)]
        [TestCase(11)]
        public async Task CorrectlySkipAndTakeWhenPageIsHigherThanMaxPage(int page)
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.CountAsync()).Returns(Task.FromResult(50));
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuidesAsync(page);
            var maxPage = 50 / 5;
            var skip = (maxPage - 1) * 5;
            var take = 5;

            guides.Verify(x => x.GetPagedGuidesAsync(skip, take));
        }

        [Test]
        public async Task CallGetHeroForEveryGuideFound()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.CountAsync()).Returns(Task.FromResult(50));
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            heroesProvider.Setup(x => x.GetHeroAsync(It.IsAny<string>())).Returns(Task.FromResult(new HeroDto { ImageUrl = "" }));
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();
            var guidesList = new List<GuideListDto> { new GuideListDto { HeroId = "1",ItemIds= new List<string>() }, new GuideListDto { HeroId = "2", ItemIds = new List<string>() }, new GuideListDto { HeroId = "3", ItemIds = new List<string>() } };
            mapper.Setup(x => x.Map<IEnumerable<GuideListDto>>(It.IsAny<object>())).Returns(guidesList);

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetGuidesAsync();

            heroesProvider.Verify(x => x.GetHeroAsync("1"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("2"), Times.Once);
            heroesProvider.Verify(x => x.GetHeroAsync("3"), Times.Once);
        }
    }
}
