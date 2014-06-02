using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using MACPortal.DAL;
using MACPortal.Models.Users;
using WebMatrix.WebData;

namespace MACPortal.Filters
{
    public class RedirectToAgreeToTerms : ActionFilterAttribute
    {
        private const bool CLOSED = false;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            using (var db = new PortalContext())
            {
                var membership = (SimpleMembershipProvider) Membership.Provider;
                var userEmployee = db.Employees.Find(membership.GetUserId(filterContext.HttpContext.User.Identity.Name));
                if (userEmployee.CurrentAcceptedAgreement != UserAgreement.CurrentVersion)
                {
                    if (filterContext.ActionDescriptor.ActionName == "PreRegister")
                    {
                        return;
                    }
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "PreRegister",
                        controller = "Member"
                    });

                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
                else if (userEmployee.AvatarHair == null)
                {
                    if (filterContext.ActionDescriptor.ActionName == "PreAvatar")
                    {
                        return;
                    }

                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "PreAvatar",
                        controller = "Member"
                    });

                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
// ReSharper disable ConditionIsAlwaysTrueOrFalse
                else if (CLOSED)
// ReSharper restore ConditionIsAlwaysTrueOrFalse
                {
                    if (filterContext.ActionDescriptor.ActionName == "PreRegisterDone" ||
                        filterContext.ActionDescriptor.ActionName == "PreAvatar")
                    {
                        return;
                    }
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "PreRegisterDone",
                        controller = "Member"
                    });

                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
                else if (filterContext.ActionDescriptor.ActionName == "PreRegister" ||
                         filterContext.ActionDescriptor.ActionName == "PreRegisterDone" ||
                         filterContext.ActionDescriptor.ActionName == "PreAvatar")
                {
                    var routeValues = new RouteValueDictionary(new
                    {
                        action = "Index",
                        controller = "Member"
                    });
                    filterContext.Result = new RedirectToRouteResult(routeValues);
                }
            }
        }
    }
}