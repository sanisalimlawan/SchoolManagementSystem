using Application.IRepo;
using Application.ViewModels;
using Application.ViewModels.Shared;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionRepo _secRepo;
        private readonly ILocationsSevices _loc;
        public SectionController(ISectionRepo secrepo,ILocationsSevices loc)
        {
            _secRepo = secrepo;
            _loc = loc;
        }
        public async Task<IActionResult> Index([FromQuery] FilterOptions filter)
        {
            ViewBag.Filter = filter;
            var data = await _secRepo.GetPaginatedList(filter);
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Create"}
            };
            ViewData["programs"] = new SelectList(await _loc.GetAllProgramAsync(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SectionViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var response = await _secRepo.CreateAsync(model, "Admin");
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            //}
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Edit"}
            };
            ViewData["programs"] = new SelectList(await _loc.GetAllProgramAsync(), "Id", "Name");
            var data = await _secRepo.GetByIdAsync(Id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SectionViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var response = await _secRepo.UpdateAsync(model, "Admin");
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            //}
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var response = await _secRepo.DeleteAsync(Id);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
