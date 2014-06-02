using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MACPortal.Models.Users
{
    public interface IUserRepository : IDisposable
    {
        void CreateNewUser(string username, string password, string role = "Member");
        void DeleteUser(string username);
        UserProfile GetUserByID(int id);
        int SaveChanges();
    }
}