using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security
{
    public static class PasswordHelper
    {
        public static bool VerifyPassword(string savedPassword, string userLoginPassword)
        {
            bool IsOk = BCrypt.Net.BCrypt.Verify(savedPassword, userLoginPassword);

            return IsOk;
        }
    }
}
