using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Providers;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Tests.Providers.HeroesProviderTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrownArgumentExceptionWhenHttpClientIsNull()
        {
            IHttpClient httpClient = null;
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var cache = new Mock<IMemoryCache>();

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient, jsonSerializer.Object,mapper.Object, cache.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenJsonSerializerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            IJsonSerializer jsonSerializer = null;
            var mapper = new Mock<IMapper>();
            var cache = new Mock<IMemoryCache>();

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer,mapper.Object, cache.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenMapperIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;
            var cache = new Mock<IMemoryCache>();

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper,cache.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenCacheIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;
            IMemoryCache cache = null;

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper, cache));
        }

        [Test]
        public void ShouldNotThrowWhenParametersAreCorrect()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            var cache = new Mock<IMemoryCache>();

            Assert.DoesNotThrow(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object,mapper.Object,cache.Object));
        }
    }
}
