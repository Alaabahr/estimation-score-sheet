using EstimasionSS.DataContext;
using EstimasionSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EstimasionSS.Controllers
{
    [Authorize]
    public class AuditController : Controller
    {
        // GET: Audit
        public async Task<ActionResult> Index()
        {
            List<AuditModel> model = await AuditHelper.PartitionScanAsync(User.Identity.Name);
            return View(model);
        }

        public async  Task<bool> Delete(string rowKey)
        {
            AuditHelper.DeleteEntityAsync(User.Identity.Name, rowKey);

            return true;
        }
    }
}