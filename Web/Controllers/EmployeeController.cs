using Application.IRepo;
using Application.ViewModels;
using Application.ViewModels.Shared;
using Infrastructure.Services.Implementation;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILocationsSevices _loc;
        private readonly IEmployeeRepo _emprepo;
        public EmployeeController(IEmployeeRepo emprepo,ILocationsSevices loc)
        {
            _emprepo=emprepo;
            _loc = loc;
        }
        public async Task<IActionResult> Index([FromQuery] FilterOptions filter)
        {
            ViewBag.Filter = filter;
            var employees = await _emprepo.GetPaginatedList(filter);
            return View(employees);
        }

        public async Task<IActionResult> Teachers([FromQuery] FilterOptions filter)
        {
            ViewBag.Filter = filter;
            var employees = await _emprepo.GetTeachersAsync(filter);
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Create"}
            }; 
            ViewData["states"] = new SelectList(await _loc.GetStatesAsync(), "Id", "Name");
            ViewData["Roles"] = new SelectList(await _loc.GetAllRoles(), "Name", "Name");
            //ViewBag.Roles =
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model,IFormFile file)
        {
            //if (ModelState.IsValid)
            //{
                var response = await _emprepo.CreateAsync(model,file);
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = response.Message;
                return View();
            //}
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            //if (ModelState.IsValid)
            //{
            var response = await _emprepo.DeleteAsync(id);
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

        public async Task<IActionResult> Details(Guid Id)
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Details"}
            };
            var details = await _emprepo.GetByIdAsync(Id);
            return View(details);
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            ViewBag.Breadcrumbs = new List<BreadCrumb>
            {
                new BreadCrumb {Title = "List", Url = Url.Action(nameof(Index))},
                new BreadCrumb {Title = "Edit"}
            };

            ViewData["states"] = new SelectList(await _loc.GetStatesAsync(), "Id", "Name");
            var data = await _emprepo.GetByIdAsync(Id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model,IFormFile file)
        {
            var result = await _emprepo.UpdateAsync(model, file);
            if (result.Status)
            {
                TempData["success"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Profile()
        {
            return View();
        }

        #region endpoints

        [HttpGet]
        [Route("/api/v1/v2/getLga")]
        public async Task<IActionResult> GetLocalGovernments(Guid sId)
        {
            var data = await _loc.GetLocalGovernmentsAsync(sId);
            List<LocalGovernmentViewModel>? lGViewModels = data.ToList();
            if (lGViewModels.Any())
            {
                return StatusCode(200, new
                {
                    Lgas = lGViewModels
                });
            }

            return StatusCode(400, new
            {
                message = "Local Governments not found"
            });
        }

        #endregion
    }
}
