using Autofac.Extras.Moq;
using Bitlink.Data.Repositories;
using Bitlink.Entities;
using Bitlink.Web.Controllers;
using Bitlink.Web.Mappings;
using Bitlink.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
                mock.Mock<IEntityBaseRepository<Link>>();

                // Arrange - configure the mock
                var controller = mock.Create<LinksController>();

                // Act
                controller.Request = new HttpRequestMessage(HttpMethod.Get, new Uri("http://localhost/"));
                controller.Configuration = new HttpConfiguration();

                var response = controller.Get();

                // Assert
                IEnumerable<LinkViewModel> linkViewModels;
                Assert.IsTrue(response.TryGetContentValue(out linkViewModels));
            }
        }

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            AutoMapperConfiguration.Configure();
        }
    }
}
