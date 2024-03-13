using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace MahmoodRezaAsadi.Web.Controllers
{
    public class ShowSubGroupsController : Controller
    {
        private ICourseService _courseService;

        public ShowSubGroupsController(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public JsonResult Index(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "گروه را انتخاب کنید" ,Value =""},
            };

            list.AddRange(_courseService.GetSubGroupsForAdminPanel(id));

            return Json(new SelectList(list.ToList(),"Value","Text"));
        }
    }
}
