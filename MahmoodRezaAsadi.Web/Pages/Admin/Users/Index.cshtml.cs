using Core.DTOs;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private IUserAdminService _userAdminService;

        public IndexModel(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        [BindProperty]
        public GetUsersForAdminPanel Users { get; set; }

        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            Users = _userAdminService.GetAllUsers(pageId,filterUserName,filterEmail);
        }
    }
}
