using Core.Convertors;
using Core.DTOs;
using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserAdminService : IUserAdminService
    {
        private DataBaseContext _context;

        public UserAdminService(DataBaseContext context)
        {
            _context = context;
        }

        public void BanUser(int userId)
        {
            var user = _context.Users.Find(userId);

            if (user != null)
            {
                user.IsBanned = true;
                _context.SaveChanges();
            }
        }

        public GetUsersForAdminPanel GetAllUsers(int pageId = 1, string filterByName = "", string filterByEmail = "")
        {
            IQueryable<User> users = _context.Users;

            if (!string.IsNullOrEmpty(filterByEmail))
            {
                users = users.Where(u => u.Email.Contains(FixedText.FixedEmail(filterByEmail)));
            }

            if (!string.IsNullOrEmpty(filterByName))
            {
                users = users.Where(u => u.UserName.Contains(filterByName));
            }

            //pagging System
            int take = 28;
            int skip = (pageId - 1) * take;

            GetUsersForAdminPanel viewModel = new GetUsersForAdminPanel()
            {
                CurrentPage = pageId,
                PageCount = users.Count() / take,
                Users = users.OrderBy(u => u.CreateDate).Skip(skip).Take(take).ToList()
            };


            return viewModel;
        }

        public User GetBannedUserById(int userId)
        {
            return _context.Users.IgnoreQueryFilters().Single(u=>u.UserId == userId);
        }

        public List<User> GetBannedUsers()
        {
            return _context.Users.IgnoreQueryFilters().Where(u => u.IsBanned).ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Single(u => u.UserId == userId);
        }

        public void RecoveryBannedUserById(int userId)
        {
            var user = _context.Users.IgnoreQueryFilters().Single(u => u.UserId == userId);

            if (user != null)
            {
                user.IsBanned = false;
                _context.SaveChanges();
            }
        }
    }
}
