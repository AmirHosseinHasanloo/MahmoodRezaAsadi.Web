using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Convertors
{
    public class FixedText
    {
        public static string FixedEmail(string email) =>
            email.Trim().ToLower();
    }
}
