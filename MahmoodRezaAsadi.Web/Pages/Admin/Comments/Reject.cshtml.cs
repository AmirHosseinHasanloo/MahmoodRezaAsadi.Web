using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Comments
{
    [RoleCheckAttribute(1)]
    public class RejectModel : PageModel
    {
        private ICourseService _courseService;


        public RejectModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet(int id)
        {
            _courseService.RejectComment(id);
        }
    }
}
