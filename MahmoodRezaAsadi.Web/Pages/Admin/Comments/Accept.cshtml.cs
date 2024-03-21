using Core.Security;
using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Comments
{
    [RoleCheckAttribute(1)]
    public class AcceptModel : PageModel
    {
        private ICourseService _courseService;


        public AcceptModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public void OnGet(int id)
        {
            _courseService.AcceptComment(id);
        }
    }
}
