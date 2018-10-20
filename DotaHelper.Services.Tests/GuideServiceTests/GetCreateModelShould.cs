using DotaHelper.Data.Interfaces;
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
    public class GetCreateModelShould
    {
        [Test]
        public async Task GetAllHeroes()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetCreateModel();

            heroesProvider.Verify(x => x.GetAllHeroesAsync());
        }

        [Test]
        public async Task GetAllItems()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.GetCreateModel();

            itemsProvider.Verify(x => x.GetAllItemsAsync());
        }
    }
}
