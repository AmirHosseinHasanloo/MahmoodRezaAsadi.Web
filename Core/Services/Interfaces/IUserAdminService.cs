using Core.DTOs;
using DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserAdminService
    {
        #region Get
        User GetUserById(int userId);
        User GetBannedUserById(int userId);
        List<User> GetBannedUsers();
        GetUsersForAdminPanel GetAllUsers(int pageId = 1, string filterByName = "", string filterByEmail = "");
        #endregion

        #region Control User

        void BanUser(int userId);
        void RecoveryBannedUserById(int userId);
        #endregion
    }
}
