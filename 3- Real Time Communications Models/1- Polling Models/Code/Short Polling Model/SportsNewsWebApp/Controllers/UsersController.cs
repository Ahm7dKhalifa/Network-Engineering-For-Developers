using Microsoft.AspNetCore.Mvc;

namespace SportsNewsWebApp.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

