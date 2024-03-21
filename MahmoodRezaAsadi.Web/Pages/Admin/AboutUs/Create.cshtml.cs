using Core.Services.Interfaces;
using DataLayer.Entities.AboutUs;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.AboutUs
{
    [RoleCheckAttribute(1)]
    public class CreateModel : PageModel
    {
        private IAboutUsService _aboutUsService;

        public CreateModel(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }


        [BindProperty]
        public DataLayer.Entities.AboutUs.AboutUs AboutUs { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_aboutUsService.GetAboutUs() == null)
            {
                _aboutUsService.AddAboutUs(AboutUs);
            }


            return Redirect("/Admin");
        }
    }
}
