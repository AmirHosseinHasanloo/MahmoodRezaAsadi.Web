using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseGroup
{
    public class EditModel : PageModel
    {
        private ICourseService _courseService;

        public EditModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }

        public void OnGet(int id)
        {
            CourseGroup = _courseService.GetCourseGroupById(id);
        }


        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _courseService.EditGroup(CourseGroup);

            return RedirectToPage("Index");
        }
    }
}
