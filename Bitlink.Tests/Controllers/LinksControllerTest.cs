using Autofac.Extras.Moq;
using Bitlink.Data.Repositories;
using Bitlink.Entities;
using Bitlink.Web.Controllers;
using Bitlink.Web.Mappings;
using Bitlink.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Bitlink.Tests.Controllers
{
    [TestClass]
    public class LinksControllerTest
    {
        [TestMethod]
        public void Get()
        {

            using (var mock = AutoMock.GetLoose())
            {
                var testLinks = new[] { new Link { Id = 1 }, new Link { Id = 2 } };

                // Arrange - configure the mock
                mock.Mock<IEntityBaseRepository<Link>>().Setup(x => x.GetAll()).Returns(() => new EnumerableQuery<Link>(testLinks));
                var controller = mock.Create<LinksController>();

                // Act
                controller.Request = new HttpRequestMessage();
                controller.Configuration = new HttpConfiguration();
                var response = controller.Get();

                // Assert
                IEnumerable<LinkViewModel> linkViewModels;
                Assert.IsTrue(response.TryGetContentValue(out linkViewModels));

                var viewModels = linkViewModels as LinkViewModel[] ?? linkViewModels.ToArray();
                Assert.IsTrue(viewModels.Any(), "Links collection is empty");

                var ids = viewModels.Select(x => x.Id).Intersect(testLinks.Select(x => x.Id));
                Assert.IsTrue(ids.Any(), "Properties are empty");
            }

        }

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
