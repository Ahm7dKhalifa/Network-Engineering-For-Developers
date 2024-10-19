using Microsoft.AspNetCore.Mvc;

namespace SportsNewsWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}