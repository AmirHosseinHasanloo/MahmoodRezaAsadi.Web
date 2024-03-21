using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.CourseGroup
{
    [RoleCheckAttribute(1)]
    public class DeleteModel : PageModel
    {
        private ICourseService _courseService;

        public DeleteModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet(int id)
        {
            _courseService.DeleteCourseGroupById(id);
        }
    }
}
