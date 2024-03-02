    using Core.Convertors;
using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.Security.Claims;
using TopLearn.Core.Generators;

namespace MahmoodRezaAsadi.Web.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;
        IViewRenderService _viewRenderService;


        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register() => View();


        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (_userService.CheckUserNameIsExists(model.UserName))
            {
                ModelState.AddModelError("UserName",
                    errorMessage: "کاربر گرامی نام کاربری ای که وارد کرده اید قبلا استفاده شده است");
                return View(model);
            }

            if (_userService.IsExistEmail(FixedText.FixedEmail(model.Email)))
            {
                ModelState.AddModelError("Email",
                   errorMessage: "کاربر گرامی ایمیلی که وارد کرده اید قبلا استفاده شده است");
                return View(model);
            }

            string salt = Bcrypt.GenerateSalt(4);

            User user = new User()
            {
                UserName = model.UserName,
                Email = FixedText.FixedEmail(model.Email),
                ActiveCode = NameGenerator.GenerateName(),
                IsActive = false,
                CreateDate = DateTime.Now,
                IsBanned = false,
                Password = Bcrypt.HashPassword(model.Password, salt),
                RoleId = 2,
                UserAvatar = "noImage.png",
            };

            _userService.AddUser(user);

            #region Send Activate Email

            string body = _viewRenderService.RenderToStringAsync("_ActivationEmail", user);
            Core.Senders.SendEmail.Send(user.Email, "ایمیل فعالسازی", body);

            #endregion

            return View("SuccessRegister", user);
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login(bool recoveryPassword = false,bool editAccount=false)
        {
            if (recoveryPassword != false)
            {
                ViewBag.IsRecovery = true;
            }

            if (editAccount != false)
            {
                ViewBag.IsEditMode = true;
            }
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(loginViewModel model, string returnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _userService.GetUserByEmail(FixedText.FixedEmail(model.Email));

            if (user != null)
            {

                if(!_userService.VerifyPassword(user.Password, model.Password))
                {

                    ModelState.AddModelError("Email"
                       , errorMessage:
                       "کاربر گرامی اطلاعات وارد شده صحیح نیست ");
                    return View(model);
                }

                if (!user.IsActive)
                {
                    ModelState.AddModelError("Email"
                       , errorMessage:
                       "کاربر گرامی حساب شما فعال نمی باشد آن را از طریق ایمیل ارسال شده فعال کنید ");
                    return View(model);
                }

                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName)
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principle = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = model.RememeberMe
                };

                HttpContext.SignInAsync(principle, properties);

                if (returnUrl != "/")
                {
                    return Redirect(returnUrl);
                }

                    ViewBag.IsSuccess = true;
                return View(model);
            }
            ModelState.AddModelError("Email"
                       , errorMessage:
                       "کاربر گرامی اطلاعات وارد شده صحیح نیست ");
            return View(model);
        }

        #endregion

        #region Active Account
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActive = _userService.ActiveAccount(id);
            return View();
        }
        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword(forgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserByEmail(FixedText.FixedEmail(model.Email));

            if (user != null)
            {
                ViewBag.IsSuccess = true;

                string body = _viewRenderService.RenderToStringAsync("_RecoveryAccountEmail", user);
                Core.Senders.SendEmail.Send(user.Email, "ایمیل بازیابی رمز عبور", body);

                return View(model);
            }
            else
            {
                ModelState.AddModelError("Email", errorMessage: "کاربری با این مشخصات یافت نشد");
                return View(model);
            }
        }
        #endregion

        #region Recovery Password
        
        public IActionResult RecoveryPassword(string id)
        {
            return View(new RecoveryPassword
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public IActionResult RecoveryPassword(RecoveryPassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.GetUserByActiveCode(model.ActiveCode);

            if (user == null)
            {
                return NotFound();
            }

            string salt = Bcrypt.GenerateSalt(4);
            user.Password = Bcrypt.HashPassword(model.Password, salt);
            _userService.UpdateUser(user);

            return Redirect("/Login?recoveryPassword=true");
        }

        #endregion

        #region Logout

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

    }
}
