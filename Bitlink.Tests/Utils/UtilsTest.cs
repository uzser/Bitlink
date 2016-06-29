using Bitlink.Web.Infrastructure.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Bitlink.Tests.Utils
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void TestUriParse()
        {
            string url;

            Assert.IsTrue(UIUtils.TryParseUrl("   http://foo:password@bitlink.azurewebsites.net:123// //Русский/", out url));

            Assert.AreEqual(url, "http://foo:password@bitlink.azurewebsites.net:123/ /Русский/", "Url parsing error");
        }
    }
}
