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
    }
}
