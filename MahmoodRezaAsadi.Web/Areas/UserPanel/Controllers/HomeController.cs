using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MahmoodRezaAsadi.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View(_userService.GetUserInfoForDashboard(User.Identity.Name));
        }

        [Route("UserPanel/Edit")]
        public IActionResult EditAccount() =>
            View(_userService.GetUserForEditAccount(User.Identity.Name));



        [Route("UserPanel/Edit")]
        [HttpPost]
        public IActionResult EditAccount(EditAccountViewModel model)
        {
            if (!ModelState.IsValid && !model.IAccept)
            {
                ModelState.AddModelError("IAccept", errorMessage: "پذیرفتن این گزینه اجباریست کاربر گرامی");
                return View(model);
            }

            _userService.UpdateUserInUserPanel(model);

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login?editAccount=true");
        }

    }
}
