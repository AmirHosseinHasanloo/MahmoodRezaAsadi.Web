using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Course
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseListForAdminPanelViewModel CourseList { get; set; }

        public void OnGet(int pageId = 1, string filterCourse = "")
        {
            CourseList = _courseService.GetAllCourseForAdminPanel(pageId, filterCourse);
        }
    }
}
