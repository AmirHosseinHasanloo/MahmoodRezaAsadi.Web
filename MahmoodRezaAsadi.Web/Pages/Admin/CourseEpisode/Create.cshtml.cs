using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    [RequestSizeLimit(524288000)]
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
