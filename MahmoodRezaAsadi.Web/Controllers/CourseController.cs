using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MahmoodRezaAsadi.Web.Controllers
{
    public class CourseController : Controller
    {
        public ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [Route("ShowCourse/{id}")]
        public IActionResult Index(int id)
        {
            return View(_courseService.GetCourseByIdForClientSide(id));
        }


        public IActionResult Filter(int pageId = 1, string filter = "", string getType = "all"
           , string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 0)
        {
            ViewBag.courseGroups = _courseService.GetCourseGroups();
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.pageId = pageId;

            return View(_courseService.ShowCourse(pageId,filter,getType,orderByType,startPrice,endPrice,selectedGroups,6));
        }
    }
}
