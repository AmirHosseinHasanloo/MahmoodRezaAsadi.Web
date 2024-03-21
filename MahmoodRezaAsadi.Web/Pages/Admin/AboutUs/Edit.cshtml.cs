using Core.Services.Interfaces;
using MahmoodRezaAsadi.Web.RoleChecker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MahmoodRezaAsadi.Web.Pages.Admin.AboutUs
{
    [RoleCheckAttribute(1)]
    public class EditModel : PageModel
    {
        private IAboutUsService _aboutUsService;

        public EditModel(IAboutUsService aboutUsService)
        {
            _aboutUsService = aboutUsService;
        }


        [BindProperty]
        public DataLayer.Entities.AboutUs.AboutUs AboutUs { get; set; }

        public void OnGet()
        {
            AboutUs = _aboutUsService.GetAboutUs();
        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_aboutUsService.GetAboutUs() != null)
            {
                _aboutUsService.UpdateAboutUs(AboutUs);
            }


            return Redirect("/Admin");
        }
    }
}
