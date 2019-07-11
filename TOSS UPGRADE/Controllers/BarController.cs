using TOSS_UPGRADE.Models;
using TOSS_UPGRADE.Models.BarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace TOSS_UPGRADE.Controllers
{
    public class BarController : Controller
    {
        DB_TOSSEntities SISDB = new DB_TOSSEntities();
        public ActionResult GetSideBar()
        {

            return PartialView("_Sidebar");
        }
        public ActionResult GetNavBar(BarModel model)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Guid id = Guid.Parse(claims.ElementAt(0).Value);

            Account tbl_Accounts = (from e in SISDB.Accounts where e.AccountID == id select e).FirstOrDefault();

            model.FullName= tbl_Accounts.PersonalInformation.FullName;
            return PartialView("_Navbar", model);
        }
    }
}