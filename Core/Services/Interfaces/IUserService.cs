using DataLayer.Entities.User;
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
        bool CheckUserNameIsExists(string userName);
        bool IsExistEmail(string email);
        void AddUser(User user);
        bool ActiveAccount(string activeCode);
        bool VerifyPassword(string savedPasword, string password);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string  activeCode);
        void UpdateUser(User user);
    }
}
