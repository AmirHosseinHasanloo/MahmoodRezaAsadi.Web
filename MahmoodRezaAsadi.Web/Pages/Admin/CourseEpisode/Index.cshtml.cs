using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public IEnumerable<DataLayer.Entities.Course.CourseEpisode> courseEpisode { get; set; }


        public void OnGet(int id, int pageId = 1, string filter = "")
        {
            var episodes = _courseService.GetEpisodesByCourseId(id, pageId, filter);

            courseEpisode = episodes.Item1.ToList();
            ViewData["CourseTitle"] = _courseService.GetCourseById(id).CourseTitle;
            ViewData["CourseId"] = id;
            ViewData["CurrentPage"] = pageId;
            ViewData["PageCount"] = episodes.Item2;
        }
    }
}
