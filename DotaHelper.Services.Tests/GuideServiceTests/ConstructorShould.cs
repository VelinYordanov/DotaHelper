using DotaHelper.Data.Interfaces;
using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Tests.GuideServiceTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void NotThrowIfAllParametersProvidedAreCorrect()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            Assert.DoesNotThrow(() => new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object));
        }

        [Test]
        public void ThorwIfDotaHelperDataIsNull()
        {
            IDotaHelperData dotaHelperData = null;
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            Assert.Throws<ArgumentException>(() => new GuidesService(dotaHelperData, heroesProvider.Object, itemsProvider.Object, mapper.Object));
        }

        [Test]
        public void ThrowIfHeroesProviderIsNull()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            IHeroesProvider heroesProvider = null;
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            Assert.Throws<ArgumentException>(() => new GuidesService(dotaHelperData.Object, heroesProvider, itemsProvider.Object, mapper.Object));
        }

        [Test]
        public void ThrowIfItemsProviderIsNull()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var heroesProvider = new Mock<IHeroesProvider>();
            IItemsProvider itemsProvider = null;
            var mapper = new Mock<IMapper>();

            Assert.Throws<ArgumentException>(() => new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider, mapper.Object));
        }

        [Test]
        public void ThrowIfMapperIsNull()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            IMapper mapper = null;

            Assert.Throws<ArgumentException>(() => new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper));
        }
    }
}
