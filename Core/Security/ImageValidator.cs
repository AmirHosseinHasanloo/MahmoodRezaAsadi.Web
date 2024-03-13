using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public static class ImageValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var Image = System.Drawing.Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
