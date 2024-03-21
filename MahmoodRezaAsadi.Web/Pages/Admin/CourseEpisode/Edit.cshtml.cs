using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    [RoleCheckAttribute(1)]
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

            var types = _courseService.GetEpisodeTipes();

            ViewData["episodeTypes"] = new SelectList(types, "Value", "Text", courseEpisode.TypeId);

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
