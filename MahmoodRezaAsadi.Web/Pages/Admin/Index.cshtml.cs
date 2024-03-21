using Core.Security;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin
{
    [RoleCheckAttribute(1)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
