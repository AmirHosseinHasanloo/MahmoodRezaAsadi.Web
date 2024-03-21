using DataLayer.Entities.AboutUs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IAboutUsService
    {

        void AddAboutUs(AboutUs aboutUs);

        void UpdateAboutUs(AboutUs aboutUs);

        AboutUs GetAboutUs();

    }
}
