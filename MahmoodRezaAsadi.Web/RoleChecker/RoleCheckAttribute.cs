using DataLayer.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MahmoodRezaAsadi.Web.RoleChecker
{
    public class RoleCheckAttribute : Attribute, IAuthorizationFilter
    {
        private int _roleId = 0;
        private DataBaseContext _context;

        public RoleCheckAttribute(int roleId)
        {
            _roleId = roleId;
        }

        public bool RoleChecker(int roleId, string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);

            if (user != null && user.RoleId == roleId)
            {
                return true;
            }

            return false;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            _context = (DataBaseContext)context.HttpContext.RequestServices.GetService(typeof(DataBaseContext));

            string userName = context.HttpContext.User.Identity.Name;

            if (!RoleChecker(_roleId, userName))
            {
                context.Result = new RedirectResult("/Login");
            }

        }
    }
}
