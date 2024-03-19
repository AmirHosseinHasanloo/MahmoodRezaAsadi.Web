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
        private IOrderService _orderService;

        public HomeController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
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

        [Route("UserPanel/UserOrders")]
        public IActionResult GetOrders(int pageId = 1)
        {
            ViewBag.CurrentPage = pageId;
            return View(_orderService.GetOrdersForUser(User.Identity.Name, pageId));
        }

        [Route("Order/{id}")]
        [Authorize]
        public IActionResult ShowOrderDetail(int id)
        {
            var order = _userService.GetUserOrderByNameForUserPanel(User.Identity.Name, id);

            if (order == null)
                return BadRequest();

            return View(order);

        }
        [Route("/UserPanel/BuyedCourses")]
        public IActionResult UserBuyedCourses()
        {
            return View(_userService.UserBuyedCourses(User.Identity.Name));
        }

        [Authorize]
        [Route("DeleteCourse/{id}")]
        public string DeleteCourseFromOrder(int id, int courseId)
        {
            _orderService.DeleteCourseFromOrder(id, courseId, User.Identity.Name);
            return _orderService.UpdateOrderPrice(id).ToString("#,0") + "تومان";
        }

        [Route("Payment/{id}")]
        [Authorize]
        public IActionResult Payment(int id)
        {
            var order = _orderService.GetOrderById(id);

            var user = _userService.GetUserByUserName(User.Identity.Name);

            var payment = new ZarinpalSandbox.Payment(order.OrderSum);

            var result = payment.PaymentRequest("خرید دوره", "https://localhost:7216/FinallyOrder/" + order.OrderId,
               user.Email, user.PhoneNumber);


            if (result.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
            return null;
        }

        [Route("FinallyOrder/{id}")]
        public IActionResult FinallyOrder(int id)
        {

            if (HttpContext.Request.Query["Status"] != "" &&
             HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
             && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var order = _orderService.GetOrderById(id);

                var payment = new ZarinpalSandbox.Payment(order.OrderSum);
                var res = payment.Verification(authority).Result;

                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;

                    // finish
                    _orderService.FinalyOrder(order.OrderId, User.Identity.Name);
                }
            }
            return View();
        }
    }
}
