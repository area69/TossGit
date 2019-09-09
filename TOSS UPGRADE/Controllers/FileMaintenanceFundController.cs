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
using TOSS_UPGRADE.Models.FM_Funds;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceFundController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenanceFund
        public ActionResult Index()
        {
            return View();
        }
        #region Fund
        //Table Fund
        public ActionResult Get_FundTable()
        {
            FM_Fund model = new FM_Fund();
            List<FundList> tbl_Fund = new List<FundList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Fund";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_FundsList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Fund.Add(new FundList()
                        {
                            FundID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FundName = GlobalFunction.ReturnEmptyString(dr[1]),
                            FundCode = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getFundList = tbl_Fund.ToList();
            return PartialView("Fund/_FundTable", model.getFundList);
        }

        //Get Add Fund Partial View
        public ActionResult Get_AddFund()
        {
            FM_Fund model = new FM_Fund();
            return PartialView("Fund/_AddFunds", model);
        }

        //Add Fund
        public JsonResult AddFunds(FM_Fund model)
        {
            Fund tblFund = new Fund();
            tblFund.FundName = model.getFundcolumns.FundName;
            tblFund.FundCode = model.getFundcolumns.FundCode;
            TOSSDB.Funds.Add(tblFund);
            TOSSDB.SaveChanges();
            return Json(tblFund);
        }

        //Get Update Fund
        public ActionResult Get_UpdateFund(FM_Fund model, int FundID)
        {
            Fund tblFund = (from e in TOSSDB.Funds where e.FundID == FundID select e).FirstOrDefault();
            model.getFundcolumns.FundID = tblFund.FundID;
            model.getFundcolumns.FundName = tblFund.FundName;
            model.getFundcolumns.FundCode = tblFund.FundCode;
            return PartialView("Fund/_UpdateFunds", model);
        }

        //Update Fund
        public ActionResult UpdateFunds(FM_Fund model)
        {
            Fund tblFund = (from e in TOSSDB.Funds where e.FundID == model.getFundcolumns.FundID select e).FirstOrDefault();
            tblFund.FundName = model.getFundcolumns.FundName;
            tblFund.FundCode = model.getFundcolumns.FundCode;
            TOSSDB.Entry(tblFund);
            TOSSDB.SaveChanges();
            return PartialView("Fund/_UpdateFunds", model);
        }

        //Delete Fund
        public ActionResult DeleteFunds(FM_Fund model, int FundID)
        {
            Fund tblFund = (from e in TOSSDB.Funds where e.FundID == FundID select e).FirstOrDefault();
            TOSSDB.Funds.Remove(tblFund);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region SubFund

        //Dropdown Fund
        public ActionResult GetDynamicFund()
        {
            FM_Fund model = new FM_Fund();
            model.SubFundList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundName = s.FundName }), "FundID", "FundName");
            return PartialView("SubFund/_DynamiccDDFundName", model);
        }
        //Dropdown Fund
        public ActionResult GetSelectedDynamicFund(int FundIDTempID)
        {
            FM_Fund model = new FM_Fund();
            model.SubFundList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundName = s.FundName }), "FundID", "FundName");
            model.SubFundNameID = FundIDTempID;
            return PartialView("SubFund/_DynamiccDDFundName", model);
        }
        //Table SubFund
        public ActionResult Get_SubFundTable()
        {
            FM_Fund model = new FM_Fund();
            List<SubFundList> tbl_Fund = new List<SubFundList>();

            var SQLQuery = "SELECT dbo.SubFund.SubFundID ,dbo.Fund.FundName ,dbo.SubFund.SubFundName FROM DB_TOSS.dbo.SubFund,dbo.Fund where dbo.Fund.FundID = dbo.SubFund.FundID";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SubFundList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Fund.Add(new SubFundList()
                        {
                            SubFundID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            FundName = GlobalFunction.ReturnEmptyString(dr[1]),
                            SubFundName = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getSubFundList = tbl_Fund.ToList();
            return PartialView("SubFund/_SubFundTable", model.getSubFundList);
        }

        //Get Add SubFund Partial View
        public ActionResult Get_AddSubFund()
        {
            FM_Fund model = new FM_Fund();
            return PartialView("SubFund/_AddSubFund", model);
        }

        //Add SubFund
        public JsonResult AddSubFunds(FM_Fund model)
        {
            SubFund tblSubFund = new SubFund();
            tblSubFund.FundID = model.SubFundNameID;
            tblSubFund.SubFundName = model.getSubFundcolumns.SubFundName;
            TOSSDB.SubFunds.Add(tblSubFund);
            TOSSDB.SaveChanges();
            return Json(tblSubFund);
        }

        //Get Update SubFund
        public ActionResult Get_UpdateSubFund(FM_Fund model, int SubFundID)
        {
            SubFund tblFund = (from e in TOSSDB.SubFunds where e.SubFundID == SubFundID select e).FirstOrDefault();
            model.getSubFundcolumns.SubFundID = tblFund.SubFundID;
            model.SubFundNameTempID = Convert.ToInt32(tblFund.FundID);
            model.getSubFundcolumns.SubFundName = tblFund.SubFundName;
            return PartialView("SubFund/_UpdateSubFund", model);
        }

        //Update SubFund
        public ActionResult UpdateSubFunds(FM_Fund model)
        {
            SubFund tblFund = (from e in TOSSDB.SubFunds where e.SubFundID == model.getSubFundcolumns.SubFundID select e).FirstOrDefault();
            tblFund.FundID = model.SubFundNameID;
            tblFund.SubFundName = model.getSubFundcolumns.SubFundName;
            TOSSDB.Entry(tblFund);
            TOSSDB.SaveChanges();
            return PartialView("SubFund/_UpdateSubFund", model);
        }

        //Delete SubFund
        public ActionResult DeleteSubFunds(FM_Fund model, int SubFundID)
        {
            SubFund tblSubFund = (from e in TOSSDB.SubFunds where e.SubFundID == SubFundID select e).FirstOrDefault();
            TOSSDB.SubFunds.Remove(tblSubFund);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        

        #endregion
    }
}