using Core.Services.Interfaces;
using DataLayer.Entities.Course;
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
            var course = _courseService.GetCourseByIdForClientSide(id);

            ViewBag.CourseTime = new TimeSpan((course.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks) == 0) ?
           0 : course.CourseEpisodes.Sum(e => e.EpisodeTime.Ticks));

            return View(course);
        }

        [HttpPost]
        public IActionResult CreateComment(CourseComment comment)
        {
            _courseService.AddComment(comment, User.Identity.Name);
            return PartialView("ShowComment", _courseService.GetCommentForCourseByCourseId(comment.CourseId.Value));
        }

        public IActionResult ShowComment(int id, int pageId = 1)
        {
            return PartialView(_courseService.GetCommentForCourseByCourseId(id, pageId));
        }

        public IActionResult Filter(int pageId = 1, string filter = "", string getType = "all"
           , string orderByType = "date", int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null, int take = 6)
        {
            ViewBag.courseGroups = _courseService.GetCourseGroups();
            ViewBag.selectedGroups = selectedGroups;
            ViewBag.pageId = pageId;

            return View(_courseService.ShowCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, take));
        }




    }
}
