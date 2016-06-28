using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Bitlink.Data;

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
    }
}
