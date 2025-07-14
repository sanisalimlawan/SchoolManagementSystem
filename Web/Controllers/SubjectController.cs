using Application.IRepo;
using Application.ViewModels;
using Application.ViewModels.Shared;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepo _subRepo;
        private readonly ILocationsSevices _loc;
        public SubjectController(ISubjectRepo subrepo, ILocationsSevices loc)
        {
            _subRepo = subrepo;
            _loc = loc;
        }
        public async Task<IActionResult> Index([FromQuery] FilterOptions filter)
        {
            ViewBag.Filter = filter;
            var data = await _subRepo.GetPaginatedList(filter);
            return View(data);
        }
        public IActionResult Index1()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Create"}
            };
            ViewData["classes"] = new SelectList(await _loc.GetAllClassesAsync(), "Id", "Name");

            ViewData["teachers"] = new SelectList(await _loc.GetAllTeachersAsync(), "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SubjectViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            var response = await _subRepo.CreateAsync(model, "Admin");
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
            ViewData["classes"] = new SelectList(await _loc.GetAllClassesAsync(), "Id", "Name");

            ViewData["teachers"] = new SelectList(await _loc.GetAllTeachersAsync(), "Id", "FullName");
            var data = await _subRepo.GetByIdAsync(Id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(SubjectViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var response = await _subRepo.UpdateAsync(model, "Admin");
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
            var response = await _subRepo.DeleteAsync(Id);
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
