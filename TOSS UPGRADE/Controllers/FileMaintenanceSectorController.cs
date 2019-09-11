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
using TOSS_UPGRADE.Models.FM_Sectors;

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceSectorController : Controller
    {
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        // GET: FileMaintenanceSector
        public ActionResult Index()
        {
            return View();
        }
        #region Sector
        //Table Sector
        public ActionResult Get_SectorTable()
        {
            FM_Sector model = new FM_Sector();
            List<SectorList> tbl_Sector = new List<SectorList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Sector";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SectorsList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Sector.Add(new SectorList()
                        {
                            SectorID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            SectorCode = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getSectorList = tbl_Sector.ToList();
            return PartialView("Sector/_SectorTable", model.getSectorList);
        }

        //Get Add Sector Partial View
        public ActionResult Get_AddSector()
        {
            FM_Sector model = new FM_Sector();
            return PartialView("Sector/_AddSectors", model);
        }

        //Add Sector
        public JsonResult AddSectors(FM_Sector model)
        {
            Sector tblSector = new Sector();
            tblSector.SectorName = model.getSectorcolumns.SectorName;
            tblSector.SectorCode = model.getSectorcolumns.SectorCode;
            TOSSDB.Sectors.Add(tblSector);
            TOSSDB.SaveChanges();
            return Json(tblSector);
        }

        //Get Update Sector
        public ActionResult Get_UpdateSector(FM_Sector model, int SectorID)
        {
            Sector tblSector = (from e in TOSSDB.Sectors where e.SectorID == SectorID select e).FirstOrDefault();
            model.getSectorcolumns.SectorID = tblSector.SectorID;
            model.getSectorcolumns.SectorName = tblSector.SectorName;
            model.getSectorcolumns.SectorCode = tblSector.SectorCode;
            return PartialView("Sector/_UpdateSectors", model);
        }

        //Update Sector
        public ActionResult UpdateSector(FM_Sector model)
        {
            Sector tblSector = (from e in TOSSDB.Sectors where e.SectorID == model.getSectorcolumns.SectorID select e).FirstOrDefault();
            tblSector.SectorName = model.getSectorcolumns.SectorName;
            tblSector.SectorCode = model.getSectorcolumns.SectorCode;
            TOSSDB.Entry(tblSector);
            TOSSDB.SaveChanges();
            return PartialView("Sector/_UpdateSectors", model);
        }

        //Delete Sector
        public ActionResult DeleteSectors(FM_Sector model, int SectorID)
        {
            Sector tblSector = (from e in TOSSDB.Sectors where e.SectorID == SectorID select e).FirstOrDefault();
            TOSSDB.Sectors.Remove(tblSector);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
        #region SubSector

        ////Dropdown SubSector
        public ActionResult GetDynamicSector()
        {
            FM_Sector model = new FM_Sector();
            model.SubSectorList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorName = s.SectorName }), "SectorID", "SectorName");
            return PartialView("SubSector/_DynamicDDSectorName", model);
        }
        public ActionResult GetDynamicSectorCode()
        {
            FM_Sector model = new FM_Sector();
            model.SubSectorList = new SelectList((from s in TOSSDB.Sectors.ToList() select new { SectorID = s.SectorID, SectorCode = s.SectorCode }), "SectorID", "SectorCode");
            return PartialView("SubSector/_DynamicLSectorCode", model);
        }
        //Dropdown SubSector
        //public ActionResult GetSelectedDynamicFund(int FundIDTempID)
        //{
        //    FM_Fund model = new FM_Fund();
        //    model.SubFundList = new SelectList((from s in TOSSDB.Funds.ToList() select new { FundID = s.FundID, FundName = s.FundName }), "FundID", "FundName");
        //    model.SubFundNameID = FundIDTempID;
        //    return PartialView("SubFund/_DynamiccDDFundName", model);
        //}
        //Table SubSector
        public ActionResult Get_SubSectorTable()
        {
            FM_Sector model = new FM_Sector();
            List<SubSectorList> tbl_ = new List<SubSectorList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.SubSector";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SubSectorsList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_.Add(new SubSectorList()
                        {
                            SubSectorID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SectorName = GlobalFunction.ReturnEmptyString(dr[1]),
                            SubSectorCode = GlobalFunction.ReturnEmptyString(dr[2]),
                            SubSectorName = GlobalFunction.ReturnEmptyString(dr[3]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getSubSectorList = tbl_.ToList();
            return PartialView("SubSector/_SubSectorTable", model.getSubSectorList);
        }
        //Get Add SubSector Partial View
        public ActionResult Get_AddSubSector()
        {
            FM_Sector model = new FM_Sector();
            return PartialView("SubSector/_AddSubSectors", model);
        }
        ////Add SubSector
        //public JsonResult AddSubFunds(FM_Fund model)
        //{
        //    SubFund tblSubFund = new SubFund();
        //    tblSubFund.FundID = model.SubFundNameID;
        //    tblSubFund.SubFundName = model.getSubFundcolumns.SubFundName;
        //    TOSSDB.SubFunds.Add(tblSubFund);
        //    TOSSDB.SaveChanges();
        //    return Json(tblSubFund);
        //}

        ////Get Update SubSector
        //public ActionResult Get_UpdateSubFund(FM_Fund model, int SubFundID)
        //{
        //    SubFund tblFund = (from e in TOSSDB.SubFunds where e.SubFundID == SubFundID select e).FirstOrDefault();
        //    model.getSubFundcolumns.SubFundID = tblFund.SubFundID;
        //    model.SubFundNameTempID = Convert.ToInt32(tblFund.FundID);
        //    model.getSubFundcolumns.SubFundName = tblFund.SubFundName;
        //    return PartialView("SubFund/_UpdateSubFund", model);
        //}

        ////Update SubSector
        //public ActionResult UpdateSubFunds(FM_Fund model)
        //{
        //    SubFund tblFund = (from e in TOSSDB.SubFunds where e.SubFundID == model.getSubFundcolumns.SubFundID select e).FirstOrDefault();
        //    tblFund.FundID = model.SubFundNameID;
        //    tblFund.SubFundName = model.getSubFundcolumns.SubFundName;
        //    TOSSDB.Entry(tblFund);
        //    TOSSDB.SaveChanges();
        //    return PartialView("SubFund/_UpdateSubFund", model);
        //}

        ////Delete SubSector
        //public ActionResult DeleteSubFunds(FM_Fund model, int SubFundID)
        //{
        //    SubFund tblSubFund = (from e in TOSSDB.SubFunds where e.SubFundID == SubFundID select e).FirstOrDefault();
        //    TOSSDB.SubFunds.Remove(tblSubFund);
        //    TOSSDB.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        #endregion
    }
}