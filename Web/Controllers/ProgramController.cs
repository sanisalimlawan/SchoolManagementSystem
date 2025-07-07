using Application.IRepo;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IProgramRepo _repo;
        public ProgramController(IProgramRepo repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> Index([FromQuery] FilterOptions filter)
        {
            ViewBag.Filter = filter;
            var data = await _repo.GetPaginatedList(filter);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description)
        {
            if (ModelState.IsValid)
            {
                 //TimeStructure timeStructureEnum = (TimeStructure)Enum.Parse(typeof(TimeStructure), timeStructure, true);
                var response = await _repo.CreateAsync(name,description);
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction("Index");
                }
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Invalid Request");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, string name, string description)
        {
            if (ModelState.IsValid)
            {
                //TimeStructure timeStructureEnum = (TimeStructure)Enum.Parse(typeof(TimeStructure), timeStructure, true);
                var response = await _repo.UpdateAsync(id, name, description);
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction("Index");
                }
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Invalid Request");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response =await _repo.DeleteAsync(id);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction("Index");
            }
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
