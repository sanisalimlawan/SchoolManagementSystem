using Application.IRepo;
using Application.ViewModels;
using Application.ViewModels.Shared;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILocationsSevices _loc;
        private readonly IEmployeeRepo _emprepo;
        public StudentController(IEmployeeRepo emprepo, ILocationsSevices loc)
        {
            _emprepo = emprepo;
            _loc = loc;
        }
        public IActionResult Index()
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

            ViewData["states"] = new SelectList(await _loc.GetStatesAsync(), "Id", "Name");
            ViewData["program"] = new SelectList(await _loc.GetAllProgramAsync(), "Id", "Name");
            //    var model = new StudentViewModel
            //    {
            //        StudentPrograms = new List<StudentProgramViewModel>
            //{
            //    new StudentProgramViewModel() // One default row
            //    };
            var model = new StudentViewModel
            {
                StudentPrograms = new List<StudentProgramViewModel>
            {
                new StudentProgramViewModel()
            }
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel model)
        {
            return View();
        }
        #region endpoints
        [HttpGet]
        [Route("/api/v1/v2/getsection")]
        public async Task<IActionResult> GetSectionByprogramId(Guid proId)
        {
            var data = await _loc.GetSectionByProgramIdAsync(proId);
            List<SectionViewModel> secViewModels = data.ToList();
            if (secViewModels.Any())
            {
                return StatusCode(200, new
                {
                    sections = secViewModels
                });
            }

            return StatusCode(400, new
            {
                message = "sections not Found"
            });
        }

        [HttpGet]
        [Route("/api/v1/v2/getclasses")]
        public async Task<IActionResult> GetClassesBySectionId(Guid sectId)
        {
            var data = await _loc.GetClassesBysectionIdAsync(sectId);
            List<ClassViewModel> classViewModels = data.ToList();
            if (classViewModels.Any())
            {
                return StatusCode(200, new
                {
                    classes = classViewModels
                });
            }

            return StatusCode(400, new
            {
                message = "class not Found"
            });
        }
        //[HttpGet]
        //[Route("/api/v1/v2/getLga")]
        //public async Task<IActionResult> GetLocalGovernments(Guid sId)
        //{
        //    var data = await _loc.GetLocalGovernmentsAsync(sId);
        //    List<LocalGovernmentViewModel>? lGViewModels = data.ToList();
        //    if (lGViewModels.Any())
        //    {
        //        return StatusCode(200, new
        //        {
        //            Lgas = lGViewModels
        //        });
        //    }

        //    return StatusCode(400, new
        //    {
        //        message = "Local Governments not found"
        //    });
        //}

#endregion
    }
}
