using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;


namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    [RequestSizeLimit(524288000)]
    [RoleCheckAttribute(1)]
    public class CreateModel : PageModel
    {
        private ICourseService _courseService;

        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.CourseEpisode courseEpisode { get; set; }

        public IActionResult OnGet(int id)
        {
            var types = _courseService.GetEpisodeTipes();

            ViewData["episodeTypes"] = new SelectList(types, "Value", "Text");
            courseEpisode = new DataLayer.Entities.Course.CourseEpisode()
            {
                CourseId = id
            };


            return Page();
        }

        public IActionResult OnPost(IFormFile video)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!_courseService.IsEpisodeExists(video.FileName))
            {

                ViewData["FileExists"] = true;
                //TODO : Add episode=>
                _courseService.AddEpisode(video, courseEpisode);
            }

            return Redirect("/Admin/CourseEpisode/" + courseEpisode.CourseId);
        }
    }
}
