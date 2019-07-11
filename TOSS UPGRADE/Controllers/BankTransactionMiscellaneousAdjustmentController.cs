using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class BankTransactionMiscellaneousAdjustmentController : Controller
    {
        // GET: BankTransactionMiscellaneousAdjustment
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetReason(int id)
        {
            return PartialView();
        }
        public ActionResult GetPreview(int id)
        {
            return PartialView();
        }
    }
}