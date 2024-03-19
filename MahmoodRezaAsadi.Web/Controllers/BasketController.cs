using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MahmoodRezaAsadi.Web.Controllers
{
    public class BasketController : Controller
    {
        private IOrderService _orderService;

        public BasketController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [Route("BuyCourse/{id}")]
        public IActionResult BuyCourse(int id)
        {
            int orderId = _orderService.AddOrder(id, User.Identity.Name);
            _orderService.UpdateOrderPrice(orderId);
            return Redirect("/Order/" + orderId);
        }
    }
}
