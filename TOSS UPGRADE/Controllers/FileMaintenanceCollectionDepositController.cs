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
        #region OR
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTable()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
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
            tblAccountableFormInventory.isIssued = false;
            tblAccountableFormInventory.Date = model.getAccountableFormInvtcolumns.Date;
            tblAccountableFormInventory.AFTableID = 1;
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
        #region CT
        //CT
        public ActionResult Get_AddInvntAccountableFormCT()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            return PartialView("InventoryofAccountableForm/CT/_AddCTDetails", model);
        }

        public ActionResult GetDynamicAccountableformCT()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.AccountableForm_Description.Description == "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/CT/_DynamicDDAFCT", model);
        }
        //Table Accountable Form Inventory
        public ActionResult Get_AccountableFormInvtTableCT()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
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
        public JsonResult AddAccountableFormInventoryCT(FM_CollectionAndDeposit_InventoryAF model)
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
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.AccountableForm_Description.Description == "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCT;
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult Get_UpdateAccountableFormInventoryCT(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
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
        public ActionResult UpdateAccountableFormInventoryCT(FM_CollectionAndDeposit_InventoryAF model)
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
        public ActionResult DeleteAccountableFormInventoryCT(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
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
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
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
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            return PartialView("InventoryofAccountableForm/CTC/_AddCTCDetails", model);
        }
        //Dropdown Accountable Form Inventory Name
        public ActionResult GetDynamicAccountableformCTC()
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.AccountableForm_Description.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("InventoryofAccountableForm/OR/_DynamicDDAccountableForm", model);
        }
        public ActionResult GetSelectedDynamicAccountableformCTC(int AFIDTempIDCTC)
        {
            FM_CollectionAndDeposit_InventoryAF model = new FM_CollectionAndDeposit_InventoryAF();
            model.AccountableFormInvtList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == true && s.AccountableForm_Description.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            model.AccountableFormInvtID = AFIDTempIDCTC;
            return PartialView("InventoryofAccountableForm/CTC/_DynamicDDAccountableFormCTC", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormInventoryCTC(FM_CollectionAndDeposit_InventoryAF model)
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
        public ActionResult Get_UpdateAccountableFormInventoryCTC(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
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
        public ActionResult UpdateAccountableFormInventoryCTC(FM_CollectionAndDeposit_InventoryAF model)
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
        public ActionResult DeleteAccountableFormInventoryCTC(FM_CollectionAndDeposit_InventoryAF model, int AFORID)
        {
            AccountableForm_Inventory tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            TOSSDB.AccountableForm_Inventory.Remove(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        //Assignment of Accountable Form
        #region Treasurer Collector
        public ActionResult Get_AddAssignTreasurerCollector()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_AddTreasurerCollector", model);
        }
        public ActionResult GetDynamicASFFundType()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.FundType_FundName.ToList() select new { FundID = s.FundID, FundTitle = s.FundTitle }), "FundID", "FundTitle");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCFundType", model);
        }
        //public ActionResult GetDynamicAFASF()
        //{
        //    FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
        //    model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Inventory.ToList() where s.AccountableFormTable.isCTC == false && s.AccountableFormTable.AccountableForm_Description.Description != "Cash Ticket" select new { AFORID = s.AFORID, AccountFormName = s.AccountableFormTable.AccountFormName }), "AFORID", "AccountFormName");
        //    return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCAF", model);
        //}
        public ActionResult GetDynamicAFASF()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableFormTables.ToList() where s.isCTC == false && s.AccountableForm_Description.Description != "Cash Ticket" select new { AccountFormID = s.AccountFormID, AccountFormName = s.AccountFormName }), "AccountFormID", "AccountFormName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDTCAF", model);
        }
        public ActionResult GetDynamicAFCollector()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.CollectorTables.ToList()  select new { CollectorID = s.CollectorID, CollectorName = s.CollectorName }), "CollectorID", "CollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDCollectorOfficer", model);
        }
        public ActionResult GetDynamicAFStartOR(int AccountFormID)
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Inventory.ToList() where s.AccountFormID == AccountFormID && s.isIssued == false select new { AFORID = s.AFORID, StartingOR = s.StartingOR }), "AFORID", "StartingOR");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_DynamicDDStartOR", model);
        }
        public ActionResult Get_AddAssignTC(int AFORID)
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            AccountableForm_Inventory tblAFIventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
            if (tblAFIventory != null)
            {
                model.AccountableFormAssignmentEndingORID = tblAFIventory.EndingOR;
                model.AccountableFormAssignmentQuantityID = tblAFIventory.Quantity;
                if (tblAFIventory.StubNo != null)
                {
                    model.AccountableFormAssignmentStubNoID = Convert.ToInt32(tblAFIventory.StubNo);
                }
                else
                {
                    model.AccountableFormAssignmentStubNoID = 0;
                }
 
            }

            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_AddTreasurerCollectorInvt", model);
        }
        //Add Accountable Form Inventory
        public JsonResult AddAccountableFormAssign(FM_CollectionAndDeposit_AssignmentAF model)
        {
            AccountableForm_Assignment tblAccountableFormAssignment = new AccountableForm_Assignment();
            tblAccountableFormAssignment.FundID = model.AccountableFormAssignmentFundID;
            tblAccountableFormAssignment.AFORID = model.AccountableFormAssignmentID;
            tblAccountableFormAssignment.CollectorID = model.AccountableFACollectorID;
            tblAccountableFormAssignment.Date = model.getAccountableFormAssigncolumns.Date;
            TOSSDB.AccountableForm_Assignment.Add(tblAccountableFormAssignment);
            TOSSDB.SaveChanges();

            AccountableForm_Inventory tblAccountableFormAssignmentInventory = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == model.AccountableFormAssignmentID select e).FirstOrDefault();
            if (tblAccountableFormAssignmentInventory.isIssued == false)
            {
                tblAccountableFormAssignmentInventory.isIssued = true;
            }
            else
            {
                tblAccountableFormAssignmentInventory.isIssued = false;
            }
            TOSSDB.Entry(tblAccountableFormAssignmentInventory);
            TOSSDB.SaveChanges();

            return Json("");
        }
        //public ActionResult GetAccountFormID(int AFORID)
        //{
        //    var AFI = (from e in TOSSDB.AccountableForm_Inventory where e.AFORID == AFORID select e).FirstOrDefault();
        //    var result = new { AccountFormID = AFI.AccountFormID };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Get_AccountableFormAssignmentTable()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            List<AccountableFormAssignmentList> tbl_AccountableFormAss = new List<AccountableFormAssignmentList>();

            var SQLQuery = "SELECT AccountableForm_Assignment.AssignAFID,CollectorTable.CollectorName,dbo.AccountableForm_Assignment.Date,AccountableForm_Inventory.StubNo,AccountableForm_Inventory.StartingOR,AccountableForm_Inventory.EndingOR,AccountableForm_Inventory.Quantity, AccountableFormTable.AccountFormName,FundType_FundName.FundTitle, dbo.AccountableForm_Assignment.IsTransferred FROM DB_TOSS.dbo.AccountableForm_Assignment,dbo.AccountableForm_Inventory,dbo.CollectorTable,dbo.FundType_FundName,AccountableFormTable where AccountableForm_Inventory.AFORID = AccountableForm_Assignment.AFORID AND dbo.CollectorTable.CollectorID = AccountableForm_Assignment.CollectorID AND dbo.FundType_FundName.FundID = dbo.AccountableForm_Assignment.FundID AND dbo.AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID and AccountableForm_Assignment.IsTransferred IS NULL ";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AccountableFormAssList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableFormAss.Add(new AccountableFormAssignmentList()
                        {
                            AssignAFID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            AF = GlobalFunction.ReturnEmptyString(dr[7]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[8]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAccountableFormAssList = tbl_AccountableFormAss.ToList();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/_TreasurerCollectorTable", model.getAccountableFormAssList);
        }
        public ActionResult Get_AddTransferReturnOR()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_AddTransferReturnOR", model);
        }
        public ActionResult Get_TransferReturnORTable()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            List<AFTransferReturnORList> tbl_AFTransferReturnOR = new List<AFTransferReturnORList>();

            var SQLQuery = "SELECT  AccountableForm_Assignment.AssignAFID,CollectorTable.CollectorName,dbo.AccountableForm_Assignment.Date,AccountableForm_Inventory.StubNo,AccountableForm_Inventory.StartingOR,AccountableForm_Inventory.EndingOR,AccountableForm_Inventory.Quantity, AccountableFormTable.AccountFormName,FundType_FundName.FundTitle, SubCollectorTable.SubCollectorName, AccountableForm_Assignment.IsTransferred FROM DB_TOSS.dbo.AccountableForm_Assignment,dbo.AccountableForm_Inventory,dbo.CollectorTable,dbo.FundType_FundName,AccountableFormTable ,dbo.SubCollectorTable where AccountableForm_Assignment.IsTransferred = 1 And AccountableForm_Inventory.AFORID = AccountableForm_Assignment.AFORID AND dbo.CollectorTable.CollectorID = AccountableForm_Assignment.CollectorID AND dbo.FundType_FundName.FundID = dbo.AccountableForm_Assignment.FundID AND dbo.AccountableFormTable.AccountFormID = AccountableForm_Inventory.AccountFormID AND dbo.AccountableForm_Assignment.SubCollectorID = SubCollectorTable.SubCollectorID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_AFTransferReturnORList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AFTransferReturnOR.Add(new AFTransferReturnORList()
                        {
                            AssignAFID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            CollectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            Date = GlobalFunction.ReturnEmptyString(dr[2]),
                            StubNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StratingOR = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingOR = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            AF = GlobalFunction.ReturnEmptyString(dr[7]),
                            FundType = GlobalFunction.ReturnEmptyString(dr[8]),
                            SubCollector = GlobalFunction.ReturnEmptyString(dr[9]),
                            IsTransferred = GlobalFunction.ReturnEmptyBool(dr[10]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getAFTransferReturnORList = tbl_AFTransferReturnOR.ToList();
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_TransferReturnORTable", model.getAFTransferReturnORList);
        }

        public ActionResult GetDynamicMainCollector()
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.CollectorTables.ToList() select new { CollectorID = s.CollectorID, CollectorName = s.CollectorName }), "CollectorID", "CollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDMainCollector", model);
        }
        public ActionResult GetDynamicSubCollector(int CollectorID)
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.SubCollectorTables.ToList() where s.SubCollectorID != CollectorID select new { SubCollectorID = s.SubCollectorID, SubCollectorName = s.SubCollectorName }), "SubCollectorID", "SubCollectorName");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDSubCollector", model);
        }
        public ActionResult GetDynamicTCTRORStubNo(int CollectorID)
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            model.AccountableFormAssignmentList = new SelectList((from s in TOSSDB.AccountableForm_Assignment.ToList() where s.CollectorID == CollectorID && s.IsTransferred == null select new { AssignAFID = s.AssignAFID, StubNo = s.AccountableForm_Inventory.StubNo}), "AssignAFID", "StubNo");
            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_DynamicDDTransferReturnORStubNo", model);
        }
        public JsonResult AddTransferReturnOR(FM_CollectionAndDeposit_AssignmentAF model)
        {
            AccountableForm_Assignment tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Assignment where e.AssignAFID == model.AccountableTCTRORStubNoID select e).FirstOrDefault();
            tblAccountableFormInventory.SubCollectorID = model.AccountableTCTRORSubCID;
            tblAccountableFormInventory.IsTransferred = true;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return Json("");
        }

        public ActionResult GetDynamicTCTRORPV(int StubNo)
        {
            FM_CollectionAndDeposit_AssignmentAF model = new FM_CollectionAndDeposit_AssignmentAF();
            AccountableForm_Inventory tblAFIventory = (from e in TOSSDB.AccountableForm_Inventory where e.StubNo == StubNo select e).FirstOrDefault();
            if (tblAFIventory != null)
            {
                model.AccountableTCTRORStartingORID = tblAFIventory.StartingOR;
                model.AccountableTCTROREndingORID = tblAFIventory.EndingOR;
                model.AccountableTCTRORQuantityID = tblAFIventory.Quantity;
            }

            return PartialView("AssignmentofAccountableForm/TreasurerCollector/TransferReturnOR/_AddTransferReturnPVOR", model);
        }

        //Get Update AF Transfer / Return OR
        public ActionResult Get_UpdateAFTransferReturnOR(FM_CollectionAndDeposit_AssignmentAF model, int AssignAFID)
        {
            AccountableForm_Assignment tblAccountableFormInventory = (from e in TOSSDB.AccountableForm_Assignment where e.AssignAFID == AssignAFID select e).FirstOrDefault();
            tblAccountableFormInventory.SubCollectorID = null;
            tblAccountableFormInventory.IsTransferred = null;
            TOSSDB.Entry(tblAccountableFormInventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        

        #endregion

        #region Barangay
        //Barangay

        //Table Barangay Name
        public ActionResult Get_BarangayNameTable()
        {
            FM_CollectionAndDeposit_BarangayName model = new FM_CollectionAndDeposit_BarangayName();
            List<BarangayNameList> tbl_AccountableForm = new List<BarangayNameList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Barangay_BarangayName";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_BarangayNameList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_AccountableForm.Add(new BarangayNameList()
                        {
                            BarangayID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            BarangayName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getBarangayNameList = tbl_AccountableForm.ToList();
            return PartialView("Barangay/BarangayName/_BarangayNameTable", model.getBarangayNameList);
        }
        //Get Add Barangay Name Partial View
        public ActionResult Get_AddBarangayName()
        {
            FM_CollectionAndDeposit_BarangayName model = new FM_CollectionAndDeposit_BarangayName();
            return PartialView("Barangay/BarangayName/_AddBarangayName", model);
        }
        //Add Barangay Name
        public JsonResult AddBarangayName(FM_CollectionAndDeposit_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = new Barangay_BarangayName();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Barangay_BarangayName.Add(tblBarangayName);
            TOSSDB.SaveChanges();
            return Json(tblBarangayName);
        }
        //Get Barangay Name
        public ActionResult Get_UpdateBarangayName(FM_CollectionAndDeposit_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            model.getBarangayNamecolumns.BarangayID = tblBarangayName.BarangayID;
            model.getBarangayNamecolumns.BarangayName = tblBarangayName.BarangayName;
            return PartialView("Barangay/BarangayName/_UpdateBarangayName", model);
        }
        public ActionResult UpdateBarangayName(FM_CollectionAndDeposit_BarangayName model)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == model.getBarangayNamecolumns.BarangayID select e).FirstOrDefault();
            tblBarangayName.BarangayName = model.getBarangayNamecolumns.BarangayName;
            TOSSDB.Entry(tblBarangayName);
            TOSSDB.SaveChanges();
            return PartialView("Barangay/BarangayName/_UpdateBarangayName", model);
        }
        //Delete Barangay Name
        public ActionResult DeleteBarangayName(FM_CollectionAndDeposit_BarangayName model, int BarangayID)
        {
            Barangay_BarangayName tblBarangayName = (from e in TOSSDB.Barangay_BarangayName where e.BarangayID == BarangayID select e).FirstOrDefault();
            TOSSDB.Barangay_BarangayName.Remove(tblBarangayName);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


        #endregion

    }
}