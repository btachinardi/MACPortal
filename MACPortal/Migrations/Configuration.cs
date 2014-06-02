using System.Web.Security;
using MACPortal.DAL;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace MACPortal.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MACPortal.DAL.PortalContext>
    {
        PortalContext db = new PortalContext();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MACPortal.DAL.PortalContext context)
        {
            //db.Database.Delete();
            //db.SaveChanges();

            WebSecurity.InitializeDatabaseConnection("PortalContext",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;

            if (!roles.RoleExists("Admin"))
            {
                roles.CreateRole("Admin");
            }
            if (!roles.RoleExists("Member"))
            {
                roles.CreateRole("Member");
            }
            if (membership.GetUser("admin", false) == null)
            {
                membership.CreateUserAndAccount("admin", "pesca160064");
            }
            if (!roles.GetRolesForUser("admin").Contains("Admin"))
            {
                roles.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            }
        }
    }
}
