
using Application.ViewModels;
using Core.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionRepo _sessionRepo;
        public SessionController(ISessionRepo sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }
        //[Authorize(Roles = RoleConstant.SuperAdmin)]
        public async Task<IActionResult> Index([FromQuery] FilterOptions options)
        {
            var data = await _sessionRepo.GetPaginatedList(options);
            ViewBag.options = options;
            return View(data);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create(string name, DateOnly startDate, DateOnly endDate)
        {
            //if(ModelState.IsValid)
            //{
                var response = await _sessionRepo.CreateAsync(name,startDate,endDate);
                if (response.Status)
                {
                    TempData["success"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["error"] = response.Message;
                return RedirectToAction(nameof(Index));
            //}
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(Guid Id, string name, DateOnly startDate, DateOnly endDate)
        {
            var response = await _sessionRepo.UpdateAsync(Id, name, startDate, endDate);
            if (response.Status)
            {
                TempData["success"] = response.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var response = await _sessionRepo.DeleteAsync(Id);
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
