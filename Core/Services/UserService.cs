using BCrypt.Net;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Generators;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private DataBaseContext _context;

        public UserService(DataBaseContext context)
        {
            _context = context;
        }

        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);

            if (user == null || user.IsActive)
            {
                return false;
            }


            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateName();
            _context.SaveChanges();
            return true;
        }

        public void AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool CheckUserNameIsExists(string userName)
        {
            if (_context.Users.Any(u => u.UserName.ToLower().Trim() == userName.ToLower().Trim()))
            {
                return true;
            }

            return false;
        }

        public bool VerifyPassword(string savedPasword, string password)
        {
            bool isOk = BCrypt.Net.BCrypt.Verify(password, savedPasword);
            return isOk;
        }

        public bool IsExistEmail(string email)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
    }
}
