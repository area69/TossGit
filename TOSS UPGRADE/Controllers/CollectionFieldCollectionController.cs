using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class CollectionFieldCollectionController : Controller
    {
        // GET: CollectionFieldCollection

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetNewButton(int ID)
        {
            return PartialView("_NewButton");
        }
        public ActionResult GetAddButton(int ID)
        {
            return PartialView("_AddButton");
        }
        public ActionResult GetSetDefaultButton(int ID)
        {
            return PartialView("_DefaultButton");
        }
        public ActionResult GetYesConfirmationButton(int ID)
        {
            return PartialView("_YesConfirmationButton");
        }
    }
}