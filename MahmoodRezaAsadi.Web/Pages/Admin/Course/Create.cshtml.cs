using Core.Services.Interfaces;
using DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Course
{
    [RequestSizeLimit(524288000)]
    public class CreateModel : PageModel
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public CreateModel(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }

        [BindProperty]
        public DataLayer.Entities.Course.Course Course { get; set; }

        public IActionResult OnGet()
        {
            var groups = _courseService.GetCourseGroupsForAdminPanel();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGroups = _courseService.GetSubGroupsForAdminPanel(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var courseStatus = _courseService.GetAllCourseStatuses();
            ViewData["CourseStatus"] = new SelectList(courseStatus, "Value", "Text");

            return Page();
        }


        public IActionResult OnPost(IFormFile image, IFormFile demo)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            Course.CourseImageName = "course_no_image.png";
            Course.CreateDate = DateTime.Now;
            int userId = _userService.GetUserByUserName(User.Identity.Name).UserId;
            Course.UserId = userId;
            // Add course with image and demo
            _courseService.AddCourse(Course, image, demo);

            return RedirectToPage("Index");
        }
    }
}
