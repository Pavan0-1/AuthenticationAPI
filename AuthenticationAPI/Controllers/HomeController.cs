using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
