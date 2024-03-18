using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Comments
{
    public class IndexModel : PageModel
    {
        ICourseService _courseService;

        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<DataLayer.Entities.Course.CourseComment> Comment { get; set; }
        public void OnGet(int id)
        {
            Comment = _courseService.GetAllCommentsByCourseId(id);
        }
    }
}
