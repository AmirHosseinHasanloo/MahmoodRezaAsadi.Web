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
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var modelStateVal = ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        // نمایش هر خطا
                        var errorMessage = error.ErrorMessage;
                        // یا استفاده از error.Exception
                    }
                }
                return Page();
            }

            _courseService.EditGroup(CourseGroup);

            return RedirectToPage("Index");
        }
    }
}
