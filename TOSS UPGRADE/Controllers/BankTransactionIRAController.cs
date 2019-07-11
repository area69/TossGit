using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class BankTransactionIRAController : Controller
    {
        // GET: BankTransactionIRA
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}