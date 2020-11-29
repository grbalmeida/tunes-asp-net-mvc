using Microsoft.AspNetCore.Mvc;

namespace Tunes.AspNet.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
