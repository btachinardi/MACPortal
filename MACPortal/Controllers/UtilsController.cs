using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MACPortal.DAL;
using MACPortal.Models;
using MACPortal.Models.Users;
using MACPortal.ViewModel;
using WebMatrix.WebData;

namespace MACPortal.Controllers
{
    public class UtilsController : Controller
    {
        PortalContext db = new PortalContext();
        //
        // GET: /Utils/

        public ActionResult Index()
        {
            return View();
        }

        /**
         * AVATAR
         */
        public ActionResult Avatar()
        {

            var employee = new Broker();
            return View(new AvatarVM().Start(employee));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Avatar(AvatarVM registration)
        {
            byte[] data = Convert.FromBase64String(registration.AvatarFile);
            return File(data, "image/png", "Avatar.png");
        }

    }
}
