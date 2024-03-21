using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Course
{
    [RoleCheckAttribute(1)]
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
