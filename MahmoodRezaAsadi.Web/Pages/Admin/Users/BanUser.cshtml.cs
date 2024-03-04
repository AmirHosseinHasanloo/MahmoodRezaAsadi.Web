using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.Users
{
    public class BanUserModel : PageModel
    {
       private IUserAdminService _userAdminService;

        public BanUserModel(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }

        [BindProperty]
        public User User { get; set; }

        public void OnGet(int id) 
        {
            User = _userAdminService.GetUserById(id);
        }


        public IActionResult OnPost()
        {
            _userAdminService.BanUser(User.UserId);

            return RedirectToPage("Index");
        }
    }
}
