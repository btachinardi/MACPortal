using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BootstrapMvcSample.Controllers;
using MACPortal.Models.Users;
using MACPortal.DAL;
using WebGrease.Css.Extensions;
using WebMatrix.WebData;

namespace MACPortal.Controllers
{
    public class ManageBrokersController : BootstrapBaseController
    {
        private PortalContext db = new PortalContext();

        //
        // GET: /ManageBrokers/

        public ActionResult Index()
        {
            var employees = db.Employees.Where(e => e is Broker).Include(b => b.User);
            return View(employees.ToList().Cast<Broker>());
        }

        //
        // GET: /ManageBrokers/Details/5

        public ActionResult Details(int id = 0)
        {
            Broker broker = (Broker)db.Employees.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        //
        // GET: /ManageBrokers/Create

        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "UserName");
            ViewBag.ManagersDropDown = CreateManagersDropDown();
            return View();
        }

        //
        // POST: /ManageBrokers/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Broker broker)
        {

            if (ModelState.IsValid)
            {
                broker.CPF = broker.CPF.Replace(".", String.Empty).Replace("-", String.Empty).Trim();
                var registerError = AccountController.RegisterEmployee(broker.CPF, broker.CPF);
                if (registerError != null)
                {   
                    ModelState.AddModelError("", registerError);
                }
                broker.UserID = WebSecurity.GetUserId(broker.CPF);
                broker.Manager = (Manager)db.Employees.Find(broker.ManagerID);
                db.Employees.Add(broker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagersDropDown = CreateManagersDropDown();
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "UserName", broker.UserID);
            return View(broker);
        }

        //
        // GET: /ManageBrokers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Broker broker = (Broker)db.Employees.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            ViewBag.ManagersDropDown = CreateManagersDropDown(broker);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "UserName", broker.UserID);
            return View(broker);
        }

        //
        // POST: /ManageBrokers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagersDropDown = CreateManagersDropDown(broker);
            ViewBag.UserID = new SelectList(db.UserProfiles, "UserID", "UserName", broker.UserID);
            return View(broker);
        }

        //
        // GET: /ManageBrokers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Broker broker = (Broker)db.Employees.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        //
        // POST: /ManageBrokers/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Broker broker = (Broker)db.Employees.Find(id);
            var membership = (SimpleMembershipProvider)Membership.Provider;
            var roles = (SimpleRoleProvider)Roles.Provider;
            roles.RemoveUsersFromRoles(new[] {broker.User.UserName}, new[]{"Member"});
            membership.DeleteAccount(broker.User.UserName);
            db.Employees.Remove(broker);
            var userProfile = db.UserProfiles.Find(id);
            db.UserProfiles.Remove(userProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }



        private Collection<SelectListItem> CreateManagersDropDown(Broker broker = null)
        {
            var managersDropDown = new Collection<SelectListItem>();
            db.Employees.Where(e => e is Manager).ToList().Cast<Manager>().ForEach(m => managersDropDown.Add(new SelectListItem
            {
                Selected = broker != null && broker.ManagerID == m.UserID,
                Text = m.Name,
                Value = Convert.ToString(m.UserID)
            }));
            return managersDropDown;
        }
    }
}