
using Application.ViewModels;
using Core.IRepo;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers
{
    public class TermController : Controller
    {
        private readonly ITermRepo _termRepo;
        private readonly ILocationsSevices _loc;
        public TermController(ITermRepo semesterRepo, ILocationsSevices loc)
        {
            _termRepo = semesterRepo;
            _loc = loc;
        }
        //[Authorize(Roles = RoleConstant.SuperAdmin)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var data = await _termRepo.GetPaginatedList(options);
            var load = await _loc.GetAllSessions();
            var selectitems = load.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.IsCurrent ? $"{x.Name} (Active)" : x.Name,
            }).ToList();
            ViewData["Sessions"] = selectitems;
            ViewBag.options = options;
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, DateOnly startDate, DateOnly endDate, Guid Sessid)
        {
            if (ModelState.IsValid)
            {
                var response = await _termRepo.CreateAsync(name, startDate, endDate,Sessid);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid Id, string name, DateOnly startDate, DateOnly endDate, Guid Sessid)
        {
            var response = await _termRepo.UpdateAsync(Id, name, startDate, endDate, Sessid);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var response = await _termRepo.DeleteAsync(Id);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        #region endpoints
        [HttpGet]
        [Route("/api/v1/sessions")]
        public async Task<IActionResult> GetAllSession()
        {
            var data = await _loc.GetAllSessions();
            if (data != null && data.Any())
            {
                return Ok( new
                {
                    List = data
                });
            }

            return StatusCode(400, new
            {
                message = "no any session in db"
            });
        }
        #endregion
    }
}
