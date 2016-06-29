using Bitlink.Data;
using Bitlink.Data.Infrastructure;
using Bitlink.Data.Repositories;
using Bitlink.Data.Repositories.Extensions;
using Bitlink.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Bitlink.Tests.Database
{
    [TestClass]
    public class DatabaseTest
    {

        [TestMethod]
        public void GetData()
        {
            using (var context = new BitlinkDbContext())
            {
                var links = context.Links.Take(2).ToArray();
                var users = links.SelectMany(x => x.Users).ToArray();
                var clicks = links.SelectMany(x => x.Clicks).ToArray();

                Assert.IsTrue(links.Any(), "Links are not found");
                Assert.IsTrue(users.Any(), "Users are not found");
                Assert.IsTrue(clicks.Any(), "Clicks are not found");
            }
        }

        [TestMethod]
        public void GetLinksByUserUid()
        {
            var linkRepository = new EntityBaseRepository<Link>(new DbFactory());
            var links = linkRepository.GetLinksByUserUid(Guid.Parse("00D84ECC-113E-4D5F-86AD-F67F79F708D0"));
            Assert.IsTrue(links.Any(), "Links are not found");
        }
    }
}
