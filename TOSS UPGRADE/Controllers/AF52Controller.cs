using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class AF52Controller : Controller
    {
        // GET: AF52
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}