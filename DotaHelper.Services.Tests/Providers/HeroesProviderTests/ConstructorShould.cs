using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Providers;
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

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient, jsonSerializer.Object,mapper.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenJsonSerializerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            IJsonSerializer jsonSerializer = null;
            var mapper = new Mock<IMapper>();

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer,mapper.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenMapperIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object, mapper));
        }

        [Test]
        public void ShouldNotThrowWhenParametersAreCorrect()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();

            Assert.DoesNotThrow(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object,mapper.Object));
        }
    }
}
