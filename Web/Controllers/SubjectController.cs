using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
