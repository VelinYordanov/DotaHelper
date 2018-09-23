using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Tests.PlayerServiceTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentExceptionWhenHttpClientIsNull()
        {
            IHttpClient httpClient = null;
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenJsonSerializerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            IJsonSerializer jsonSerializer = null;
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer, mapper.Object, heroesProvider.Object, itemsProvider.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenMockerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper, heroesProvider.Object, itemsProvider.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenHeroesProviderIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;
            IHeroesProvider heroesProvider =null;
            var itemsProvider = new Mock<IItemsProvider>();
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper, heroesProvider, itemsProvider.Object));
        }

        [Test]
        public void ShouldThrowWhenItemsProviderIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            IItemsProvider itemsProvider = null;
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider));
        }

        [Test]
        public void ShouldNotThrowWhenAllParametersAreCorrect()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            Assert.DoesNotThrow(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object, heroesProvider.Object, itemsProvider.Object));
        }
    }
}
