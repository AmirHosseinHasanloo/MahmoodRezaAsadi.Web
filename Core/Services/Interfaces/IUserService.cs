using Core.DTOs;
using DataLayer.Entities.Order;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IUserService
    {

        #region Get
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        User GetUserByUserName(string userName);
        SideBarViewModel GetUserSideBarInfo(string userName);
        DashboardInfoViewModel GetUserInfoForDashboard(string userName);
        EditAccountViewModel GetUserForEditAccount(string userName);

        #endregion

        #region Check
        bool CheckUserNameIsExists(string userName);
        bool VerifyPassword(string savedPasword, string password);
        bool IsExistEmail(string email);
        bool ActiveAccount(string activeCode);
        #endregion

        #region CRUD
        void AddUser(User user);
        void UpdateUser(User user);
        void UpdateUserInUserPanel(EditAccountViewModel model);
        #endregion


        #region UserOrder

        Order GetUserOrderByNameForUserPanel(string userName,int orderId);

        List<UserCourses> UserBuyedCourses(string userName);

        #endregion
    }
}
