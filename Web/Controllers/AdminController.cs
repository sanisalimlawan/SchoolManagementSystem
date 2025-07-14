using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Setting()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
