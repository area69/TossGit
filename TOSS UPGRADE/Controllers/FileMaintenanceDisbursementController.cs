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
using TOSS_UPGRADE.Models.FM_Disbursement;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceDisbursementController : Controller
    {
        // GET: FileMaintenanceDisbursement
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        //Check Inventory
        #region 

        //Dropdown Account Name
        public ActionResult GetDynamicAccountName(int BankAccountID)
        {
            FM_Disbursement_CheckInventory model = new FM_Disbursement_CheckInventory();
            model.CheckInventoryAccountNameList = new SelectList((from s in TOSSDB.BankAccountTables.ToList() where s.BankID == BankAccountID select new { BankAccountID = s.BankAccountID, AccountNo = s.AccountNo + "- ("+ s.FundType_FundName.FundTitle +")" }), "BankAccountID", "AccountNo");
            return PartialView("CheckInventory/_DynamicDDAccountName", model);
        }
        public ActionResult GetSelectedDynamicAccountName(int CheckInventoryAccountNameIDTempID)
        {
            FM_Disbursement_CheckInventory model = new FM_Disbursement_CheckInventory();
            model.CheckInventoryAccountNameList = new SelectList((from s in TOSSDB.BankAccountTables.ToList() where s.BankAccountID == CheckInventoryAccountNameIDTempID select new { BankAccountID = s.BankAccountID, AccountNo = s.AccountNo + "- (" + s.FundType_FundName.FundTitle + ")" }), "BankAccountID", "AccountNo");
            model.CheckInventoryAccountNameID = CheckInventoryAccountNameIDTempID;
            return PartialView("CheckInventory/_DynamicDDAccountName", model);
        }
        //Table Internal Revenue Allotment
        public ActionResult Get_CheckInventoryTable()
        {
            FM_Disbursement_CheckInventory model = new FM_Disbursement_CheckInventory();
            List<CheckInventoryList> tbl_CheckInventory = new List<CheckInventoryList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.CheckInventoryTable,dbo.BankAccountTable,dbo.BankTable where dbo.BankAccountTable.BankAccountID = dbo.CheckInventoryTable.BankAccountID and dbo.BankTable.BankID = dbo.BankAccountTable.BankID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckInventoryList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_CheckInventory.Add(new CheckInventoryList()
                        {
                            CheckInvntID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            Bank = GlobalFunction.ReturnEmptyString(dr[17]),
                            AccountName = GlobalFunction.ReturnEmptyString(dr[11]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[3]),
                            StartingChckNo = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingChckNo = GlobalFunction.ReturnEmptyInt(dr[5]),
                            Date = Convert.ToDateTime(dr[6]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getCheckInventoryList = tbl_CheckInventory.ToList();
            return PartialView("CheckInventory/_CheckInventoryTable", model.getCheckInventoryList);
        }

        //Get Add DVType Partial View
        public ActionResult Get_AddCheckInventory()
        {
            FM_Disbursement_CheckInventory model = new FM_Disbursement_CheckInventory();
            return PartialView("CheckInventory/_AddCheckInventory", model);
        }

        //Add DVType
        public JsonResult AddCheckInventory(FM_Disbursement_CheckInventory model)
        {
            CheckInventoryTable tblCheckInventory= new CheckInventoryTable();
            tblCheckInventory.BankID = model.getCheckInventorycolumns.BankID;
            tblCheckInventory.BankAccountID = model.CheckInventoryAccountNameID;
            tblCheckInventory.Quantity = model.getCheckInventorycolumns.Quantity;
            tblCheckInventory.StartingChckNo = model.getCheckInventorycolumns.StartingChckNo;
            tblCheckInventory.EndingChckNo = model.getCheckInventorycolumns.EndingChckNo;
            tblCheckInventory.Date = model.getCheckInventorycolumns.Date;
            tblCheckInventory.IsIssued = false;
            TOSSDB.CheckInventoryTables.Add(tblCheckInventory);
            TOSSDB.SaveChanges();
            return Json(tblCheckInventory);
        }
        
        //Get Update DVType
        public ActionResult Get_UpdateCheckInventory(FM_Disbursement_CheckInventory model, int CheckInvntID)
        {
            CheckInventoryTable tblCheckIventory = (from e in TOSSDB.CheckInventoryTables where e.CheckInvntID == CheckInvntID select e).FirstOrDefault();
            model.getCheckInventorycolumns.CheckInvntID = tblCheckIventory.CheckInvntID;
            model.getCheckInventorycolumns.BankID = tblCheckIventory.BankID;
            model.CheckInventoryAccountNameTempID = tblCheckIventory.BankAccountID;
            model.getCheckInventorycolumns.Quantity = tblCheckIventory.Quantity;
            model.getCheckInventorycolumns.StartingChckNo = tblCheckIventory.StartingChckNo;
            model.getCheckInventorycolumns.EndingChckNo = tblCheckIventory.EndingChckNo;
            model.getCheckInventorycolumns.Date = tblCheckIventory.Date;
            return PartialView("CheckInventory/_UpdateCheckInventory", model);
        }

        //Update DVType
        public ActionResult UpdateCheckInventory(FM_Disbursement_CheckInventory model)
        {
            CheckInventoryTable tblCheckIventory = (from e in TOSSDB.CheckInventoryTables where e.CheckInvntID == model.getCheckInventorycolumns.CheckInvntID select e).FirstOrDefault();
            tblCheckIventory.BankID = model.getCheckInventorycolumns.BankID;
            tblCheckIventory.BankAccountID = model.CheckInventoryAccountNameID;
            tblCheckIventory.Quantity = model.getCheckInventorycolumns.Quantity;
            tblCheckIventory.StartingChckNo = model.getCheckInventorycolumns.StartingChckNo;
            tblCheckIventory.EndingChckNo = model.getCheckInventorycolumns.EndingChckNo;
            tblCheckIventory.Date = model.getCheckInventorycolumns.Date;
            TOSSDB.Entry(tblCheckIventory);
            TOSSDB.SaveChanges();
            return PartialView("CheckInventory/_UpdateCheckInventory", model);
        }

        //Delete DVType
        public ActionResult DeleteCheckInventory(FM_Disbursement_CheckInventory model, int CheckInvntID)
        {
            CheckInventoryTable tblCheckIventory = (from e in TOSSDB.CheckInventoryTables where e.CheckInvntID == CheckInvntID select e).FirstOrDefault();
            TOSSDB.CheckInventoryTables.Remove(tblCheckIventory);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        //Check Maintenance
        #region
        //Dropdown Account Name
        public ActionResult GetDynamicMaitenance(int BankAccountID)
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            model.CheckMaintenanceAccountNameList = new SelectList((from s in TOSSDB.BankAccountTables.ToList() where s.BankID == BankAccountID select new { BankAccountID = s.BankAccountID, AccountNo = s.AccountNo + "- (" + s.FundType_FundName.FundTitle + ")" }), "BankAccountID", "AccountNo");
            return PartialView("CheckMaintenance/_DynamicDDAccountName2", model);
        }
        public ActionResult GetSelectedDynamicMaitenance(int CheckMaintenanceBankIDTempID)
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            model.CheckMaintenanceAccountNameList = new SelectList((from s in TOSSDB.CheckInventoryTables.ToList() where s.CheckInvntID == CheckMaintenanceBankIDTempID select new { CheckInvntID = s.CheckInvntID, AccountNo = s.BankAccountTable.AccountNo + "- (" + s.BankAccountTable.FundType_FundName.FundTitle + ")" }), "CheckInvntID", "AccountNo");
            model.CheckMaintenanceAccountNameID = CheckMaintenanceBankIDTempID;
            return PartialView("CheckMaintenance/_DynamicDDAccountName2", model);
        }
        //Get Add Check Maintenance Partial View
        public ActionResult Get_AddCheckMaitenance()
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            return PartialView("CheckMaintenance/_AddCheckMaintenance", model);
        }
        public ActionResult Get_AddCheckMaintenanceInventory()
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            return PartialView("CheckMaintenance/_DynamicDDCheckInventory", model);
        }
        public ActionResult LoadDynamicCheckMaintenanceInventory(int BankID, int AccountNameIDDD2)
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            model.CheckMaintenanceInventoryList = new SelectList((from s in TOSSDB.CheckInventoryTables.ToList() where s.IsIssued != true && s.BankID == BankID && s.BankAccountID == AccountNameIDDD2 select new { CheckInvntID = s.CheckInvntID, StartingChckNo = s.StartingChckNo }), "CheckInvntID", "StartingChckNo");
            return PartialView("CheckMaintenance/_DynamicDDStartingCheckNo", model);
        }
        public ActionResult Get_CheckMaintenanceQuantyNEnd(int BankID,int BankAccountID,int StartingChckNo)
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();

                CheckInventoryTable tblCheckIventory = (from e in TOSSDB.CheckInventoryTables where e.BankID == BankID && e.BankAccountID == BankAccountID && e.StartingChckNo == StartingChckNo select e).FirstOrDefault();
                if (tblCheckIventory != null)
                {
                    model.CheckMaintenanceEndingNoID = tblCheckIventory.EndingChckNo;
                    model.CheckMaintenanceQuantityID = tblCheckIventory.Quantity;
                }
    

            return PartialView("CheckMaintenance/_DynamicTBQuantyNEndNo", model);
        }

        //Add  Check Maintenance
        public JsonResult AddCheckMaintenance(FM_Disbursement_CheckMaintenance model)
        {
            CheckMaintenanceTable tblCheckMaintenance = new CheckMaintenanceTable();
            tblCheckMaintenance.CheckInvntID = model.CheckMaintenanceInventoryID;
            TOSSDB.CheckMaintenanceTables.Add(tblCheckMaintenance);
            TOSSDB.SaveChanges();

            CheckInventoryTable tblAccountablechkInventory = (from e in TOSSDB.CheckInventoryTables where e.IsIssued == false select e).FirstOrDefault();
            if (tblAccountablechkInventory.IsIssued == false)
            {
                tblAccountablechkInventory.IsIssued = true;
            }
            else
            {
                tblAccountablechkInventory.IsIssued = false;
            }
            TOSSDB.Entry(tblAccountablechkInventory);
            TOSSDB.SaveChanges();

            return Json("");
        }
        //Get Table Check Maintenance
        public ActionResult Get_CheckMaintenanceTable()
        {
            FM_Disbursement_CheckMaintenance model = new FM_Disbursement_CheckMaintenance();
            List<CheckMaintenanceList> tbl_CheckMaintenance = new List<CheckMaintenanceList>();

            var SQLQuery = "SELECT CheckMaintenanceTable.CheckMainteID,CheckMaintenanceTable.CheckInvntID,BankAccountTable.AccountNo,BankTable.BankName,CheckInventoryTable.StartingChckNo,CheckInventoryTable.EndingChckNo ,CheckInventoryTable.Quantity FROM CheckMaintenanceTable,CheckInventoryTable,BankAccountTable,BankTable WHERE CheckInventoryTable.CheckInvntID =CheckMaintenanceTable.CheckInvntID and BankAccountTable.BankAccountID=CheckInventoryTable.BankAccountID and BankTable.BankID=BankAccountTable.BankID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_CheckMaintenanceList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_CheckMaintenance.Add(new CheckMaintenanceList()
                        {
                            CheckMainteID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            Bank = GlobalFunction.ReturnEmptyString(dr[3]),
                            AccountName = GlobalFunction.ReturnEmptyString(dr[2]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[6]),
                            StartingChckNo = GlobalFunction.ReturnEmptyInt(dr[4]),
                            EndingChckNo = GlobalFunction.ReturnEmptyInt(dr[5]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getCheckMaintenanceList = tbl_CheckMaintenance.ToList();
            return PartialView("CheckMaintenance/_CheckMaintenanceTable", model.getCheckMaintenanceList);
        }
        //Get Update Check Maintenance
        public ActionResult Get_UpdateCheckMaintenance(FM_Disbursement_CheckMaintenance model, int CheckMainteID)
        {
            CheckMaintenanceTable tblCheckMaintenance = (from e in TOSSDB.CheckMaintenanceTables where e.CheckMainteID == CheckMainteID select e).FirstOrDefault();
            model.getCheckMaintenancecolumns.CheckMainteID = tblCheckMaintenance.CheckMainteID;
            model.CheckMaintenanceBankIDTemp = tblCheckMaintenance.CheckInvntID;
            return PartialView("CheckMaintenance/_UpdateCheckMaintenance", model);
        }
        //Update Check Maintenance

        //Delete Check Maintenance
        public ActionResult DeleteCheckMaintenance(FM_Disbursement_CheckMaintenance model, int CheckMainteID)
        {
            CheckMaintenanceTable tblCheckMaintenance = (from e in TOSSDB.CheckMaintenanceTables where e.CheckMainteID == CheckMainteID select e).FirstOrDefault();
            TOSSDB.CheckMaintenanceTables.Remove(tblCheckMaintenance);
            TOSSDB.SaveChanges();

            CheckInventoryTable tblAccountablechkInventory = (from e in TOSSDB.CheckInventoryTables where e.IsIssued == true select e).FirstOrDefault();
            if (tblAccountablechkInventory.IsIssued == true)
            {
                tblAccountablechkInventory.IsIssued = false;
            }
            else
            {
                tblAccountablechkInventory.IsIssued = true;
            }
            TOSSDB.Entry(tblAccountablechkInventory);
            TOSSDB.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        //DV Type
        #region
        //Table Internal Revenue Allotment
        public ActionResult Get_DVTypeTable()
        {
            FM_Disbursement_DVType model = new FM_Disbursement_DVType();
            List<DVTypeList> tbl_DVType = new List<DVTypeList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.DVTypeTable";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_DVTypeList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_DVType.Add(new DVTypeList()
                        {
                            DVTypeID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            DVTypeName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getDVTypeList = tbl_DVType.ToList();
            return PartialView("DVType/_DVTypeTable", model.getDVTypeList);
        }

        //Get Add DVType Partial View
        public ActionResult Get_AddDVType()
        {
            FM_Disbursement_DVType model = new FM_Disbursement_DVType();
            return PartialView("DVType/_AddDVType", model);
        }

        //Add DVType
        public JsonResult AddDVType(FM_Disbursement_DVType model)
        {
            DVTypeTable tblDvType = new DVTypeTable();
            tblDvType.DVTypeName = model.getDVTypecolumns.DVTypeName;
            TOSSDB.DVTypeTables.Add(tblDvType);
            TOSSDB.SaveChanges();
            return Json(tblDvType);
        }

        //Get Update DVType
        public ActionResult Get_UpdateDVType(FM_Disbursement_DVType model, int DVTypeID)
        {
            DVTypeTable tblDVType = (from e in TOSSDB.DVTypeTables where e.DVTypeID == DVTypeID select e).FirstOrDefault();
            model.getDVTypecolumns.DVTypeID = tblDVType.DVTypeID;
            model.getDVTypecolumns.DVTypeName = tblDVType.DVTypeName;
            return PartialView("DVType/_UpdateDVType", model);
        }

        //Update DVType
        public ActionResult UpdateDVType(FM_Disbursement_DVType model)
        {
            DVTypeTable tblDVType = (from e in TOSSDB.DVTypeTables where e.DVTypeID == model.getDVTypecolumns.DVTypeID select e).FirstOrDefault();
            tblDVType.DVTypeName = model.getDVTypecolumns.DVTypeName;
            TOSSDB.Entry(tblDVType);
            TOSSDB.SaveChanges();
            return PartialView("DVType/_UpdateDVType", model);
        }

        //Delete DVType
        public ActionResult DeleteDVType(FM_Disbursement_DVType model, int DVTypeID)
        {
            DVTypeTable tblDvType = (from e in TOSSDB.DVTypeTables where e.DVTypeID == DVTypeID select e).FirstOrDefault();
            TOSSDB.DVTypeTables.Remove(tblDvType);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}