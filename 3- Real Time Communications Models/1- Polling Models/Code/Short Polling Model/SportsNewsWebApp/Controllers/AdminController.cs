using Microsoft.AspNetCore.Mvc;

namespace SportsNewsWebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

