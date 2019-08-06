using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOSS_UPGRADE.GlobalFunctions;
using TOSS_UPGRADE.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using TOSS_UPGRADE.Models.FM_CollectionAndDeposit;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceCollectionDepositController : Controller
    {
        // GET: FileMaintenanceCollectionDeposit
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        //Accountable Form
        #region
        //Table Accountable Form
        public ActionResult Get_AccountableFormTable()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            List<AccountableFormList> tbl_AccountableForm = new List<AccountableFormList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableFormTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new AccountableFormList()
                        {
                            AccountFormID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AccountFormName = GlobalFunction.ReturnEmptyString(dr[1]),
                            isCTC = GlobalFunction.ReturnEmptyBool(dr[3]),
                            Description = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormList = tbl_AccountableForm.ToList();
            return PartialView("AccountableForm/_AccountableFormTable", model.getAccountableFormList);
        }

        //Get Add DVType Partial View
        public ActionResult Get_AddAccountableForm()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            return PartialView("AccountableForm/_AddAccountableForm", model);
        }

        //Add DVType
        public JsonResult AddAccountableForm(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = new AccountableFormTable();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.AccountableFormTables.Add(tblAccountableForm);
            TOSSDB.SaveChanges();
            return Json(tblAccountableForm);
        }


        //Get Update DVType
        public ActionResult Get_UpdateAccountableForm(FM_CollectionAndDeposit_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            model.getAccountableFormcolumns.AccountFormID = tblAccountableForm.AccountFormID;
            model.getAccountableFormcolumns.AccountFormName = tblAccountableForm.AccountFormName;
            model.getAccountableFormcolumns.isCTC = tblAccountableForm.isCTC;
            model.getAccountableFormcolumns.Description = tblAccountableForm.Description;
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Update DVType
        public ActionResult UpdateAccountableForm(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == model.getAccountableFormcolumns.AccountFormID select e).FirstOrDefault();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.Entry(tblAccountableForm);
            TOSSDB.SaveChanges();
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Delete DVType
        public ActionResult DeleteAccountableForm(FM_CollectionAndDeposit_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            TOSSDB.AccountableFormTables.Remove(tblAccountableForm);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        //Inventory of Accountable Form
        #region

        #endregion
    }
}