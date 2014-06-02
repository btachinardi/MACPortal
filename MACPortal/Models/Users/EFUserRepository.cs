using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MACPortal.DAL;
using WebMatrix.WebData;

namespace MACPortal.Models.Users
{
    public class EFUserRepository : IUserRepository
    {
        private readonly PortalContext db = new PortalContext();
        private readonly SimpleMembershipProvider memberships;
        private readonly SimpleRoleProvider roles;

        public EFUserRepository(SimpleMembershipProvider memberships, SimpleRoleProvider roles)
        {
            this.memberships = memberships;
            this.roles = roles;
        }

        public void CreateNewUser(string username, string password, string role = "Member")
        {
            memberships.CreateUserAndAccount(username, password);
            roles.AddUsersToRoles(new[] { username }, new[] { role });
        }

        public void DeleteUser(string username)
        {
            var userRoles = roles.GetRolesForUser(username);
            foreach (var userRole in userRoles)
            {
                roles.RemoveUsersFromRoles(new[] { username }, new[] { userRole });
            }
            memberships.DeleteAccount(username);
            memberships.DeleteUser(username, true);
        }

        public UserProfile GetUserByID(int id)
        {
            return db.UserProfiles.FirstOrDefault(u => u.UserID == id);
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}