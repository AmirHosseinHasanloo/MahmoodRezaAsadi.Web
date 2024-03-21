using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseGroup
{
    [RoleCheckAttribute(1)]
    public class IndexModel : PageModel
    {
        private ICourseService _CourseService;

        public IndexModel(ICourseService courseService)
        {
            _CourseService = courseService;
        }

        [BindProperty]
        public List<DataLayer.Entities.Course.CourseGroup> courseGroups { get; set; }

        public void OnGet()
        {
            courseGroups = _CourseService.GetCourseGroups();
        }
    }
}
