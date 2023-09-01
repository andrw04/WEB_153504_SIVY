using Microsoft.AspNetCore.Mvc;

namespace WEB_153504_SIVY.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
