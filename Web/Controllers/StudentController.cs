using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
