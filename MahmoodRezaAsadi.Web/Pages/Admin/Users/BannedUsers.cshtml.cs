using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Users
{
    [RoleCheckAttribute(1)]
    public class BannedUsersModel : PageModel
    {
        private IUserAdminService _userAdminService;

        public BannedUsersModel(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;   
        }

        [BindProperty]
        public List<User> Users { get; set; }


        public void OnGet()
        {
            Users = _userAdminService.GetBannedUsers();
        }
    }
}
