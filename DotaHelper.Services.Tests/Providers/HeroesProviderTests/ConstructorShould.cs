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

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient, jsonSerializer.Object));
        }

        [Test]
        public void ThrownArgumentExceptionWhenJsonSerializerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            IJsonSerializer jsonSerializer = null;

            Assert.Throws<ArgumentException>(() => new HeroesProvider(httpClient.Object, jsonSerializer));
        }

        [Test]
        public void ShouldNotThrowWhenParametersAreCorrect()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();

            Assert.DoesNotThrow(() => new HeroesProvider(httpClient.Object, jsonSerializer.Object));
        }
    }
}
