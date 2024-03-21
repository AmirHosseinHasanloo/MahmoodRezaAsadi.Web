using BCrypt.Net;
using Core.DTOs;
using Core.Security;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.Order;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public SideBarViewModel GetUserSideBarInfo(string userName)
        {
            return _context.Users.Where(u => u.UserName == userName).Select(u => new SideBarViewModel()
            {
                RegisterDate = u.CreateDate,
                UserAvatar = u.UserAvatar,
                UserName = u.UserName,
            }).Single();
        }

        public DashboardInfoViewModel GetUserInfoForDashboard(string userName)
        {
            var user = _context.Users.Single(u => u.UserName == userName);

            DashboardInfoViewModel viewModel = new DashboardInfoViewModel
            {
                UserName = user.UserName,
                CreateDate = user.CreateDate,
                Email = user.Email,
                IsActive = user.IsActive == true ? "فعال" : "غیرفعال",
            };

            return viewModel;
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName);
        }

        public EditAccountViewModel GetUserForEditAccount(string userName)
        {
            User user = _context.Users.Single(u => u.UserName == userName);

            EditAccountViewModel viewModel = new EditAccountViewModel()
            {
                UserId = user.UserId,
                ImageName = user.UserAvatar,
                UserName = user.UserName,
                IAccept = false,
            };

            return viewModel;
        }

        public void UpdateUserInUserPanel(EditAccountViewModel model)
        {

            var user = _context.Users.Single(u => u.UserId == model.UserId);

            if (user != null)
            {
                if (model.Profile != null)
                {
                    string avatarPath = Path.Combine(Directory.GetCurrentDirectory()
                  , "wwwroot/UserAvatar/");

                    string currentAvatar = avatarPath + model.ImageName;

                    if (user.UserAvatar != "noImage.png" && Path.Exists(currentAvatar))
                    {
                        System.IO.File.Delete(currentAvatar);
                    }
                    string newProfileName = model.ImageName = NameGenerator.GenerateName() + Path.GetExtension(model.Profile.FileName);

                    string newAvatar = avatarPath + newProfileName;

                    using (var stream = new FileStream(newAvatar, FileMode.Create))
                    {
                        model.Profile.CopyTo(stream);
                    }

                    user.UserAvatar = newProfileName;
                }
                if (model.PhoneNumber != null)
                {
                    user.PhoneNumber = model.PhoneNumber;
                }
                user.UserName = model.UserName;


                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public Order GetUserOrderByNameForUserPanel(string userName, int orderId)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            return _context.Orders.Include(o => o.OrderDetails)
                .ThenInclude(o => o.Course)
                .FirstOrDefault(o => o.UserId == userId && o.OrderId == orderId);
        }

        public List<UserCourses> UserBuyedCourses(string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;


            return _context.UserCourses.Include(c => c.Course).Where(c => c.UserId == userId).ToList();
        }

        public bool IsUserBuyedCourse(int courseId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserId;

            return _context.UserCourses.Any(d => d.UserId == userId && d.CourseId == courseId);
        }
    }
}
