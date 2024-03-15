using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MahmoodRezaAsadi.Web.ViewComponents
{
    public class LatestCoursesComponent : ViewComponent
    {
        private ICourseService _courseService;

        public LatestCoursesComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("/Views/Shared/Components/LatestCoursesComponent/LatestCourses.cshtml", _courseService.ShowCourse()));
        }

    }
}
