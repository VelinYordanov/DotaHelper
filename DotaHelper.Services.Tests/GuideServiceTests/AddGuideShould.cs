using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Models.PostModels;
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
    public class AddGuideShould
    {
        [Test]
        public async Task GetTheCurrentUser()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<List<Guide>>();
            var user = new DotaHelperUser { PostedGuides =  postedGuides.Object};
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.AddGuide("1", new GuidePostDataModel());

            users.Verify(x => x.FindAsync("1"), Times.Once);
        }

        [Test]
        public async Task AddTheGuideToTheUser()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<ICollection<Guide>>();
            var user = new DotaHelperUser { PostedGuides = postedGuides.Object };
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.AddGuide("1", new GuidePostDataModel());

            postedGuides.Verify(x => x.Add(It.IsAny<Guide>()), Times.Once);
        }

        [Test]
        public async Task SaveChanges()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<List<Guide>>();
            var user = new DotaHelperUser { PostedGuides = postedGuides.Object };
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guides = new Mock<IGuideData>();
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.AddGuide("1", new GuidePostDataModel());

            dotaHelperData.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
