using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class DisbursementDisbursingOfficerController : Controller
    {
        // GET: DisbursementDisbursingOfficer

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetConsolidation(int ID)
        {
            return PartialView("_ConsolidationButtonModal");
        }
    }
}