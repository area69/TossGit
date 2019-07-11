using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class DisbursementCheckCancellationController : Controller
    {
        // GET: DisbursementCheckCancellation

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}