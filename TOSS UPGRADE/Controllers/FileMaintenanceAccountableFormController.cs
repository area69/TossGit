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
using TOSS_UPGRADE.Models.FM_AccountableForm;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceAccountableFormController : Controller
    {
        // GET: FileMaintenanceAccountableForm
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #region Accountable Form Details
        //Table Accountable Form
        public ActionResult Get_AccountableFormTable()
        {
            FM_AccountableForm model = new FM_AccountableForm();
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
        //Get Add Accountable Form Partial View
        public ActionResult Get_AddAccountableForm()
        {
            FM_AccountableForm model = new FM_AccountableForm();
            return PartialView("AccountableForm/_AddAccountableForm", model);
        }

        //Add Accountable Form
        public JsonResult AddAccountableForm(FM_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = new AccountableFormTable();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.AccountableFormTables.Add(tblAccountableForm);
            TOSSDB.SaveChanges();
            return Json(tblAccountableForm);
        }

        //Get Update Accountable Form
        public ActionResult Get_UpdateAccountableForm(FM_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            model.getAccountableFormcolumns.AccountFormID = tblAccountableForm.AccountFormID;
            model.getAccountableFormcolumns.AccountFormName = tblAccountableForm.AccountFormName;
            model.getAccountableFormcolumns.isCTC = tblAccountableForm.isCTC;
            model.getAccountableFormcolumns.Description = tblAccountableForm.Description;
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Update Accountable Form
        public ActionResult UpdateAccountableForm(FM_AccountableForm model)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == model.getAccountableFormcolumns.AccountFormID select e).FirstOrDefault();
            tblAccountableForm.AccountFormName = model.getAccountableFormcolumns.AccountFormName;
            tblAccountableForm.isCTC = model.getAccountableFormcolumns.isCTC;
            tblAccountableForm.Description = model.getAccountableFormcolumns.Description;
            TOSSDB.Entry(tblAccountableForm);
            TOSSDB.SaveChanges();
            return PartialView("AccountableForm/_UpdateAccountableForm", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableForm(FM_AccountableForm model, int AccountFormID)
        {
            AccountableFormTable tblAccountableForm = (from e in TOSSDB.AccountableFormTables where e.AccountFormID == AccountFormID select e).FirstOrDefault();
            TOSSDB.AccountableFormTables.Remove(tblAccountableForm);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region Inventory of Accountable Form
        #region OR
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTable()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 1 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
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
                            AF = GlobalFunction.ReturnEmptyString(dr[10]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyString(dr[6]),
                            isIssued = GlobalFunction.ReturnEmptyBool(dr[7])
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
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/OR/_AddORDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableform()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }

        public ActionResult GetSelectedDynamicAccountableform(int AFIDTempID)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempID;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventory(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.isIssued = false;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            tblAccountableFormInventory.AFTableID = 1;
            TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json(tblAccountableFormInventory);
        }
        //Get Position Update
        public ActionResult Get_UpdateAccountableFormInventory(FM_AccountableFormInventory model, int AFORID)
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
        public ActionResult UpdateAccountableFormInventory(FM_AccountableFormInventory model)
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
        public ActionResult DeleteAccountableFormInventory(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region CT
        //CT
        public ActionResult Get_AddInvntAccountableFormCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/CT/_AddCTDetails", model);
        }

        public ActionResult GetDynamicAccountableformCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description == "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/CT/_DynamicDDAFCT", model);
        }
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTableCT()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 2 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
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
                            AF = GlobalFunction.ReturnEmptyString(dr[10]),
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
            return PartialView("InventoryofAccountableForm/CT/_CTDetailsTable", model.getAccountableFormInvtList);
        }

        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventoryCT(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            tblAccountableFormInventory.AFTableID = 2;
            TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json(tblAccountableFormInventory);
        }
        //Get Position Update
        public ActionResult GetSelectedDynamicAccountableformCT(int AFIDTempIDCT)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.Description == "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCT;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult Get_UpdateAccountableFormInventoryCT(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/CT/_UpdateCTDetails", model);
        }
        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventoryCT(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.getAccountableFormInvtcolumns.AFORID select e).FirstOrDefault();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return PartialView("InventoryofAccountableForm/CT/_UpdateCTDetails", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventoryCT(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #region CTC
        //CTC
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTableCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            List<AccountableFormInvtList> tbl_AccountableFormInvt = new List<AccountableFormInvtList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.AccountableForm_Inventory,dbo.AccountableFormTable where AccountableForm_Inventory.AFTableID = 3 and AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID ";
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
                            AF = GlobalFunction.ReturnEmptyString(dr[10]),
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
            return PartialView("InventoryofAccountableForm/CTC/_CTCDetailsTable", model.getAccountableFormInvtList);
        }
        //Get Add Accountable Form Inventory Partial View
        public ActionResult Get_AddInvntAccountableFormCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            return PartialView("InventoryofAccountableForm/CTC/_AddCTCDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableformCTC()
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult GetSelectedDynamicAccountableformCTC(int AFIDTempIDCTC)
        {
            FM_AccountableFormInventory model = new FM_AccountableFormInventory();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCTC;
            return PartialView("InventoryofAccountableForm/CTC/_DynamicDDAccountableFormCTC", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventoryCTC(FM_AccountableFormInventory model)
        {
            AccountableForm_Inventory tblAccountableFormInventory = new AccountableForm_Inventory();
            tblAccountableFormInventory.AccountFormID = model.AccountableFormInvtID;
            tblAccountableFormInventory.StubNo = model.getAccountableFormInvtcolumns.StubNo;
            tblAccountableFormInventory.Quantity = model.getAccountableFormInvtcolumns.Quantity;
            tblAccountableFormInventory.StartingOR = model.getAccountableFormInvtcolumns.StartingOR;
            tblAccountableFormInventory.EndingOR = model.getAccountableFormInvtcolumns.EndingOR;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            tblAccountableFormInventory.AFTableID = 3;
            TOSSDB.AccountableForm_Inventory.Add(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json(tblAccountableFormInventory);
        }
        //Get Position Update
        public ActionResult Get_UpdateAccountableFormInventoryCTC(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            model.getAccountableFormInvtcolumns.AFORID = tblAccountableFormInventory.AFORID;
            model.AFTempID = tblAccountableFormInventory.AccountFormID;
            model.getAccountableFormInvtcolumns.StubNo = tblAccountableFormInventory.StubNo;
            model.getAccountableFormInvtcolumns.StartingOR = tblAccountableFormInventory.StartingOR;
            model.getAccountableFormInvtcolumns.EndingOR = tblAccountableFormInventory.EndingOR;
            model.getAccountableFormInvtcolumns.Quantity = tblAccountableFormInventory.Quantity;
            model.getAccountableFormInvtcolumns.Date = tblAccountableFormInventory.Date;
            return PartialView("InventoryofAccountableForm/CTC/_UpdateCTCDetails", model);
        }

        //Update Accountable Form
        public ActionResult UpdateAccountableFormInventoryCTC(FM_AccountableFormInventory model)
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
            return PartialView("InventoryofAccountableForm/CTC/_UpdateCTCDetails", model);
        }

        //Delete Accountable Form
        public ActionResult DeleteAccountableFormInventoryCTC(FM_AccountableFormInventory model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        #endregion

    }
}