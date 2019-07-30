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
        //Table Internal Revenue Allotment
        public ActionResult Get_CheckInventoryTable()
        {
            FM_Disbursement_CheckInventory model = new FM_Disbursement_CheckInventory();
            List<CheckInventoryList> tbl_CheckInventory = new List<CheckInventoryList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.CheckInventoryTable,dbo.BankAccountTable where dbo.BankAccountTable.BankAccountID = dbo.CheckInventoryTable.BankAccountID";
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
                            Bank = GlobalFunction.ReturnEmptyString(dr[7]),
                            AccountName = GlobalFunction.ReturnEmptyString(dr[9]),
                            Quantity = GlobalFunction.ReturnEmptyInt(dr[2]),
                            StartingChckNo = GlobalFunction.ReturnEmptyInt(dr[3]),
                            EndingChckNo = GlobalFunction.ReturnEmptyInt(dr[4]),
                            Date = Convert.ToDateTime(dr[5]),
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
            //tblCheckInventory.BankAccountID = model.getDVTypecolumns.DVTypeName;
            TOSSDB.CheckInventoryTables.Add(tblCheckInventory);
            TOSSDB.SaveChanges();
            return Json(tblCheckInventory);
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