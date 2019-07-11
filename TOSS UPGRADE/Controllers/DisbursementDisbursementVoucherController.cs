using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class DisbursementDisbursementVoucherController : Controller
    {
        // GET: DisbursementDisbursementVoucher

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetYesConfirmationButton(int ID)
        {
            return PartialView("_YesConfirmationButton");
        }
        public ActionResult GetSuccessfulButton(int ID)
        {
            return PartialView("_SuccessfulButton");
        }
    }
}