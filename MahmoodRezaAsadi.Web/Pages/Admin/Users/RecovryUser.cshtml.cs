using Core.Services.Interfaces;
using DataLayer.Entities.User;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Users
{
    [RoleCheckAttribute(1)]
    public class RecovryUserModel : PageModel
    {
        private IUserAdminService _userAdminService;

        public RecovryUserModel(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        [BindProperty]
        public User User { get; set; }

        public void OnGet(int id)
        {
            User = _userAdminService.GetBannedUserById(id);
        }

        public IActionResult OnPost()
        {
            _userAdminService.RecoveryBannedUserById(User.UserId);

            return RedirectToPage("Index");
        }
    }
}
