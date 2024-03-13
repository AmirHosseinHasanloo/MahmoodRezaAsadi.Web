using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Course
{
    public class DeleteModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteModel(ICourseService courseService)
        {
            _courseService = courseService;
        }


        [BindProperty]
        public DeleteCourseAdminViewModel Delete { get; set; }
        public void OnGet(int id)
        {
            Delete = _courseService.GetCourseForDeleteInAdminPanel(id);
        }


        public IActionResult OnPost()
        {
            //Delete Course from Database
            _courseService.DeleteCourseById(Delete.CourseId);

            return RedirectToPage("Index");
        }
    }
}
