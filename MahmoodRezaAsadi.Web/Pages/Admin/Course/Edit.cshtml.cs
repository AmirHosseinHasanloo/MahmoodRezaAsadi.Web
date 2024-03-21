using Core.Security;
using Core.Services;
using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Course
{
    [RoleCheckAttribute(1)]
    public class EditModel : PageModel
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public EditModel(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Course Course { get; set; }


        public void OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);
            var groups = _courseService.GetCourseGroupsForAdminPanel();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", Course.GroupId);

            var subGroups = _courseService.GetSubGroupsForAdminPanel(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", Course.SubGroupId ?? 0);

            var courseStatus = _courseService.GetAllCourseStatuses();
            ViewData["CourseStatus"] = new SelectList(courseStatus, "Value", "Text", Course.StatusId);
        }


        public IActionResult OnPost(IFormFile? image, IFormFile? demo)
        {
            if (!ModelState.IsValid)
                return Page();

            // EditCourse
            _courseService.UpdateCourse(Course,image,demo);

            return RedirectToPage("Index");
        }
    }
}
