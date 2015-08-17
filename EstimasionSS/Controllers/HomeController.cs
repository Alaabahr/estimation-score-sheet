using EstimasionSS.DataContext;
using EstimasionSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EstimasionSS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string userName;

            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                userName = "AnonymousUser";
            }
            AuditModel audit = new AuditModel(userName);
            audit.ActionTaken = "User entered Home>Index";

            AuditHelper.AddAudit(audit);

            return View();
        }

        public ActionResult About()
        {
            string userName;

            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                userName = "AnonymousUser";
            }
            AuditModel audit = new AuditModel(userName);

            audit.ActionTaken = "User entered Home>About";

            AuditHelper.AddAudit(audit);

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string userName;

            if (User.Identity.IsAuthenticated)
            {
                userName = User.Identity.Name;
            }
            else
            {
                userName = "AnonymousUser";
            }
            AuditModel audit = new AuditModel(userName);

            audit.ActionTaken = "User entered Home>Contact";

            AuditHelper.AddAudit(audit);

            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}