using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseEpisode
{
    public class DeleteModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public Core.DTOs.DeleteEpisodeViewModel DeleteDTO { get; set; }

        public void OnGet(int id)
        {
            DeleteDTO = _courseService.GetCourseEpisodeForDelete(id);
        }


        public IActionResult OnPost()
        {
            int CourseId = DeleteDTO.CourseId;
            _courseService.DeleteEpisodeById(DeleteDTO.EpisodeId);

            return Redirect("/Admin/Course");
        }
    }
}
