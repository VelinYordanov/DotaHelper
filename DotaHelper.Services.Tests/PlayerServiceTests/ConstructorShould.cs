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
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient, jsonSerializer.Object, mapper.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenJsonSerializerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            IJsonSerializer jsonSerializer = null;
            var mapper = new Mock<IMapper>();
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer, mapper.Object));
        }

        [Test]
        public void ThrowArgumentExceptionWhenMockerIsNull()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            IMapper mapper = null;
            Assert.Throws<ArgumentException>(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper));
        }

        [Test]
        public void ShouldNotThrowWhenAllParametersAreCorreect()
        {
            var httpClient = new Mock<IHttpClient>();
            var jsonSerializer = new Mock<IJsonSerializer>();
            var mapper = new Mock<IMapper>();
            Assert.DoesNotThrow(() => new PlayerService(httpClient.Object, jsonSerializer.Object, mapper.Object));
        }
    }
}
