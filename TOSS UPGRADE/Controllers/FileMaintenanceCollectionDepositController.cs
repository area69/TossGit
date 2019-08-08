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

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableFormTable,dbo.AccountableForm_Description where dbo.AccountableForm_Description.DescriptionID = dbo.AccountableFormTable.DescriptionID";
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
                            Description = GlobalFunction.ReturnEmptyString(dr[5]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormList = tbl_AccountableForm.ToList();
            return PartialView("AccountableForm/_AccountableFormTable", model.getAccountableFormList);
        }
        //Dropdown Accountable Form Description
        public ActionResult GetDynamicAccountableformDescription()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            model.DescriptionList = new SelectList((from s in TOSSDB.AccountableForm_Description.ToList() select new { DescriptionID = s.DescriptionID, Description = s.Description }), "DescriptionID", "Description");
            return PartialView("AccountableForm/_DynamicDDDescription", model);
        }
        public ActionResult GetSelectedDynamicAccountableformDescription(int DescriptionIDTempID)
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            model.DescriptionList = new SelectList((from s in TOSSDB.AccountableForm_Description.ToList() select new { DescriptionID = s.DescriptionID, Description = s.Description }), "DescriptionID", "Description");
            model.DescriptionID = DescriptionIDTempID;
            return PartialView("AccountableForm/_DynamicDDDescription", model);
        }

        //Get Add Accountable Form Description Partial View
        public ActionResult Get_AddDescription()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            return PartialView("AccountableForm/Description/_AddDescription", model);
        }

        //Add Accountable Form Description
        public JsonResult AddDescription(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableForm_Description tblDescription = new AccountableForm_Description();
            tblDescription.Description = model.getDescriptionColumns.Description;
            TOSSDB.AccountableForm_Description.Add(tblDescription);
            TOSSDB.SaveChanges();
            return Json(tblDescription);
        }

        //Get Position Update
        public ActionResult Get_UpdateDescription(FM_CollectionAndDeposit_AccountableForm model, int DescriptionID)
        {
            AccountableForm_Description tblDescription = (from e in TOSSDB.AccountableForm_Description where e.DescriptionID == DescriptionID select e).FirstOrDefault();
            model.getDescriptionColumns.DescriptionID = tblDescription.DescriptionID;
            model.getDescriptionColumns.Description = tblDescription.Description;
            return PartialView("AccountableForm/Description/_UpdateDescription", model);
        }

        public ActionResult UpdateDescription(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableForm_Description tblDescription = (from e in TOSSDB.AccountableForm_Description where e.DescriptionID == model.getDescriptionColumns.DescriptionID select e).FirstOrDefault();
            tblDescription.Description = model.getDescriptionColumns.Description;
            TOSSDB.Entry(tblDescription);
            TOSSDB.SaveChanges();
            return PartialView("AccountableForm/Description/_UpdateDescription", model);
        }
        //Delete Position Table
        public ActionResult DeleteDescription(FM_CollectionAndDeposit_AccountableForm model, int DescriptionID)
        {
            AccountableForm_Description tblDescription = (from e in TOSSDB.AccountableForm_Description where e.DescriptionID == DescriptionID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Description.Remove(tblDescription);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        //Table Accountable Form
        public ActionResult Get_AFDescriptionTable()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            List<AccountableForm_Description> tbl_AccountableForm = new List<AccountableForm_Description>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Description";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AFDescriptionList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new AccountableForm_Description()
                        {
                            DescriptionID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            Description = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getDescription = tbl_AccountableForm.ToList();
            return PartialView("AccountableForm/Description/_DescriptionTable", model.getDescription);
        }
        //Get Add Accountable Form Partial View
        public ActionResult Get_AddAccountableForm()
        {
            FM_CollectionAndDeposit_AccountableForm model = new FM_CollectionAndDeposit_AccountableForm();
            return PartialView("AccountableForm/_AddAccountableForm", model);
        }

        //Add Accountable Form
        public JsonResult AddAccountableForm(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = new AccountableFormTable();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.DescriptionID = model.DescriptionID;
            TOSSDB.AccountableFormTables.Add(tblAccountableForm);
            TOSSDB.SaveChanges();
            return Json(tblAccountableForm);
        }

        //Get Update Accountable Form
        public ActionResult Get_UpdateAccountableForm(FM_CollectionAndDeposit_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            model.getAccountableFormcolumns.AccountFormID = tblAccountableForm.AccountFormID;
            model.getAccountableFormcolumns.AccountFormName = tblAccountableForm.AccountFormName;
            model.getAccountableFormcolumns.isCTC = tblAccountableForm.isCTC;
            model.DescriptionTempID = tblAccountableForm.DescriptionID;
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Update Accountable Form
        public ActionResult UpdateAccountableForm(FM_CollectionAndDeposit_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == model.getAccountableFormcolumns.AccountFormID select e).FirstOrDefault();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.DescriptionID = model.DescriptionID;
            TOSSDB.Entry(tblAccountableForm);
            TOSSDB.SaveChanges();
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Delete Accountable Form
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
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTable()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormInvtList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormInvt.Add(new AccountableFormInvtList()
                        {
                            AFORID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyString(dr[9]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyString(dr[6]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormInvtList = tbl_AccountableFormInvt.ToList();
            return PartialView("InventoryofAccountableForm/OR/_ORDetailsTable", model.getAccountableFormInvtList);
        }
        //Get Add Accountable Form Inventory Partial View
        public ActionResult Get_AddInvntAccountableForm()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            return PartialView("InventoryofAccountableForm/OR/_AddORDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableform()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.AccountableForm_Description.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }

        public ActionResult GetSelectedDynamicAccountableform(int AFIDTempID)
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.AccountableForm_Description.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempID;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventory(FM_CollectionAndDeposit_InventoryAF model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json(tblAccountableFormInventory);
        }
        //Get Position Update
        public ActionResult Get_UpdateAccountableFormInventory(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StubNo = tblAccountableFormInventory.StubNo;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/OR/_UpdateORDetails", model);
        }
        
        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventory(FM_CollectionAndDeposit_InventoryAF model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.getAccountableFormInvtcolumns.AFORID select e).FirstOrDefault();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return PartialView("InventoryofAccountableForm/OR/_UpdateORDetails", model);
        }
        
        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventory(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        //
    }
}