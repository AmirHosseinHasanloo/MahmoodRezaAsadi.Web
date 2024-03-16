using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    [RequestSizeLimit(524288000)]
    public class EditModel : PageModel
    {
        private ICourseService _courseService;

        public EditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.CourseEpisode courseEpisode { get; set; }

        public IActionResult OnGet(int id)
        {
            courseEpisode = _courseService.GetEpisodeByEpisodeId(id);

            return Page();
        }

        public IActionResult OnPost(IFormFile? video)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
                _courseService.UpdateEpisode(video, courseEpisode);

            return Redirect("/Admin/CourseEpisode/" + courseEpisode.CourseId);
        }
    }
}
