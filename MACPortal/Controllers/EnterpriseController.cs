using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using MACPortal.DAL;
using MACPortal.Filters;
using MACPortal.Models;

namespace MACPortal.Controllers
{
    [AuthorizeUser(Roles = "Admin")]
    public class EnterpriseController : BootstrapBaseController
    {
        PortalContext db = new PortalContext();

        public ActionResult Index()
        {
            return View(db.Enterprises.ToList());
        }

        [HttpPost]
        public ActionResult Create(Enterprise model)
        {
            if (ModelState.IsValid)
            {
                model.EnterpriseID = !db.Enterprises.Any() ? 1 : db.Enterprises.Select(x => x.EnterpriseID).Max() + 1;
                db.Enterprises.Add(model);
                db.SaveChanges();
                Success("Your information was saved!");
                return RedirectToAction("Index");
            }
            Error("there were some errors in your form.");
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new Enterprise());
        }

        public ActionResult Delete(int EnterpriseID)
        {
            db.Enterprises.Remove(db.Enterprises.First(e => e.EnterpriseID == EnterpriseID));
            db.SaveChanges();
            Information("Item was deleted");
            if (!db.Enterprises.Any())
            {
                Attention("You have deleted all the models!");
            }
            return RedirectToAction("index");
        }
        public ActionResult Edit(int EnterpriseID)
        {
            var model = db.Enterprises.First(e => e.EnterpriseID == EnterpriseID);
            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(Enterprise model, int EnterpriseID)
        {
            if (ModelState.IsValid)
            {
                db.Enterprises.Remove(db.Enterprises.First(e => e.EnterpriseID == EnterpriseID));
                model.EnterpriseID = EnterpriseID;
                db.Enterprises.Add(model);
                db.SaveChanges();
                Success("The model was updated!");
                return RedirectToAction("index");
            }
            return View("Create", model);
        }

        public ActionResult Details(int EnterpriseID)
        {
            var model = db.Enterprises.First(e => e.EnterpriseID == EnterpriseID);
            return View(model);
        }
    }
}
