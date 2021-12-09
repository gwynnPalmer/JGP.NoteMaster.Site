using Microsoft.AspNetCore.Mvc;

namespace JGP.NoteMaster.Site.Areas.Standard.Controllers
{
    [Area("standard")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
