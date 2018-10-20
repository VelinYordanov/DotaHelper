using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
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
    public class FavoriteGuideShould
    {
        [Test]
        public async Task CallSaveChanges()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<List<Guide>>();
            var user = new DotaHelperUser { PostedGuides = postedGuides.Object, Id = "3" };
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guide = new Guide { Id = "5" };
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.FindAsync(It.IsAny<string>())).Returns(Task.FromResult(guide));
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.FavoriteGuide("3", "5");

            dotaHelperData.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddGuideToUserFavoritesIfNotFavoritted()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<List<Guide>>();
            var user = new DotaHelperUser { PostedGuides = postedGuides.Object, Id = "3" };
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guide = new Guide { Id = "5" };
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.FindAsync(It.IsAny<string>())).Returns(Task.FromResult(guide));
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.FavoriteGuide("3", "5");
            userGuides.Verify(x => x.Add(It.IsAny<DotaHelperUserGuide>()), Times.Once);
            userGuides.Verify(x => x.Remove(It.IsAny<DotaHelperUserGuide>()), Times.Never);
        }

        [Test]
        public async Task RemoveGuideFromUserFavoritesIfAlreadyFavorited()
        {
            var dotaHelperData = new Mock<IDotaHelperData>();
            var users = new Mock<IDotaHelperRepository<DotaHelperUser>>();
            var postedGuides = new Mock<List<Guide>>();
            var user = new DotaHelperUser { PostedGuides = postedGuides.Object, Id = "3", FavoritedGuides = new List<DotaHelperUserGuide> { new DotaHelperUserGuide { GuideId="5"} } };
            users.Setup(x => x.FindAsync(It.IsAny<object>())).Returns(Task.FromResult(user));
            var userGuides = new Mock<IDotaHelperRepository<DotaHelperUserGuide>>();
            var guide = new Guide { Id = "5" };
            var guides = new Mock<IGuideData>();
            guides.Setup(x => x.FindAsync(It.IsAny<string>())).Returns(Task.FromResult(guide));
            dotaHelperData.Setup(x => x.Users).Returns(users.Object);
            dotaHelperData.Setup(x => x.Guides).Returns(guides.Object);
            dotaHelperData.Setup(x => x.UserGuides).Returns(userGuides.Object);
            var heroesProvider = new Mock<IHeroesProvider>();
            var itemsProvider = new Mock<IItemsProvider>();
            var mapper = new Mock<IMapper>();

            var guideService = new GuidesService(dotaHelperData.Object, heroesProvider.Object, itemsProvider.Object, mapper.Object);

            await guideService.FavoriteGuide("3", "5");
            userGuides.Verify(x => x.Add(It.IsAny<DotaHelperUserGuide>()), Times.Never);
            userGuides.Verify(x => x.Remove(It.IsAny<DotaHelperUserGuide>()), Times.Once);
        }
    }
}
