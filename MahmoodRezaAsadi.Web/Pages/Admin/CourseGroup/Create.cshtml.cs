using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseGroup
{
    public class CreateModel : PageModel
    {
        private ICourseService _courseService;

        public CreateModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.CourseGroup CourseGroup { get; set; }

        public void OnGet(int id)
        {
            if (id != 0)
            {
                CourseGroup = new DataLayer.Entities.Course.CourseGroup()
                {
                    ParentId = id
                };
            }
        }


        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Add Group
            _courseService.AddGroup(CourseGroup);

            return RedirectToPage("Index");
        }
    }
}
