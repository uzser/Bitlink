using System.Web.Mvc;

namespace Bitlink.Web.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult Error404()
        {
            return View();
        }
    }
}
