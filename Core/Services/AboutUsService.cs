using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.AboutUs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AboutUsService : IAboutUsService
    {
        private DataBaseContext _context;

        public AboutUsService(DataBaseContext context)
        {
            _context = context;
        }

        public void AddAboutUs(AboutUs aboutUs)
        {
            _context.AboutUs.Add(aboutUs);
            _context.SaveChanges();
        }

        public AboutUs GetAboutUs()
        {
            return _context.AboutUs.FirstOrDefault();
        }

        public void UpdateAboutUs(AboutUs aboutUs)
        {
            var about = _context.AboutUs.FirstOrDefault(a => a.Id == aboutUs.Id);
            about.Description = aboutUs.Description;

            _context.AboutUs.Update(about);
            _context.SaveChanges();
        }
    }
}
