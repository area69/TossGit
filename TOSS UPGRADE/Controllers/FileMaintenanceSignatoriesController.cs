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

namespace TOSS_UPGRADE.Controllers
{
    public class FileMaintenanceSignatoriesController : Controller
    {
        // GET: FileMaintenanceSignatories
        DB_TOSSEntities TOSSDB = new DB_TOSSEntities();
        DatatypeValidation GlobalFunction = new DatatypeValidation();
        [Authorize]
        public ActionResult Index()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return View();
        }
        //Position in Dropdown
        public ActionResult GetDynamicSignatories()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.PositionList = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName = s.PositionName }), "PositionID", "PositionName");
            return PartialView("_DynamicDDPositionName", model);
        }
        public ActionResult GetSelectedDynamicSignatories(int PositionIDTempID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.PositionList = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName = s.PositionName }), "PositionID", "PositionName");
            model.PositionID = PositionIDTempID;
            return PartialView("_DynamicDDPositionName", model);
        }
        //Department in Dropdown
        public ActionResult GetDynamicDepartment()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, Department = s.Department }), "DepartmentID", "Department");
            return PartialView("_DynamicDDDepartment", model);
        }
        public ActionResult GetSelectedDynamicDepartment(int DepartmentIDTempID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            model.DepartmentList = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() select new { DepartmentID = s.DepartmentID, Department = s.Department }), "DepartmentID", "Department");
            model.DepartmentID = DepartmentIDTempID;
            return PartialView("_DynamicDDDepartment", model);
        }
        //Signatories Table Partial View
        public ActionResult GetListOfSignatories()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            List<SignatoriesList> TblsignatoriesLists = new List<SignatoriesList>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.SignatoriesTable,DB_TOSS.dbo.Signatory_PositionTable,DB_TOSS.dbo.Signatory_DepartmentTable where DB_TOSS.dbo.Signatory_PositionTable.PositionID = DB_TOSS.dbo.SignatoriesTable.PositionID and DB_TOSS.dbo.Signatory_DepartmentTable.DepartmentID = DB_TOSS.dbo.SignatoriesTable.DepartmentID order by SignatoriesName asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SignatoryTable]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        TblsignatoriesLists.Add(new SignatoriesList()
                        {
                            SignatoriesID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            SignatoriesName = GlobalFunction.ReturnEmptyString(dr[1]),
                            PositionNames = GlobalFunction.ReturnEmptyString(dr[6]),
                            Department = GlobalFunction.ReturnEmptyString(dr[8]),
                            DepartmentCode = GlobalFunction.ReturnEmptyString(dr[9]),
                            idDeptHead = GlobalFunction.ReturnEmptyBool(dr[4]),
                        });
                    }
                }
                Connection.Close();
            }

            model.getSignatoriesList = TblsignatoriesLists.ToList();


            return PartialView("_SignatoriesTable", model.getSignatoriesList);
        }

        //Add Signatories
        public JsonResult AddSignatories(FM_SignatoriesModel model)
        {
            SignatoriesTable tblSignatories = new SignatoriesTable();
            tblSignatories.SignatoriesName = GlobalFunction.ReturnEmptyString(model.getSignatoriesColumns.SignatoriesName);
            tblSignatories.PositionID = GlobalFunction.ReturnEmptyInt(model.PositionID);
            tblSignatories.DepartmentID = GlobalFunction.ReturnEmptyInt(model.DepartmentID);
            tblSignatories.IsDeptHead = GlobalFunction.ReturnEmptyBool(model.isDeptHeads);
            TOSSDB.SignatoriesTables.Add(tblSignatories);
            TOSSDB.SaveChanges();
            return Json(tblSignatories);
        }
        //Get Signatories Add Partial View
        public ActionResult Get_SignatoriesAdd()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return PartialView("_SignatoriesAdd", model);
        }
        
        //Get Signature Data
        public ActionResult Get_UpdateSignatories(FM_SignatoriesModel model, int SignatoriesID)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            model.getSignatoriesColumns.SignatoriesID = tblSignatories.SignatoriesID;
            model.getSignatoriesColumns.SignatoriesName = tblSignatories.SignatoriesName;
            model.DepartmentTempID = tblSignatories.DepartmentID;
            model.PositionTempID = tblSignatories.PositionID;
            model.isDeptHeads = tblSignatories.IsDeptHead;
            return PartialView("_SignatoriesUpdate", model);
        }

        //Update Signature Data
        public ActionResult UpdateSignatories(FM_SignatoriesModel model)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == model.getSignatoriesColumns.SignatoriesID select e).FirstOrDefault();
            tblSignatories.SignatoriesName = model.getSignatoriesColumns.SignatoriesName;
            tblSignatories.PositionID = model.PositionID;
            tblSignatories.DepartmentID = model.DepartmentID;
            tblSignatories.IsDeptHead = model.isDeptHeads;
            TOSSDB.Entry(tblSignatories);
            TOSSDB.SaveChanges();
            return PartialView("_SignatoriesUpdate", model);
        }

        //Delete Signature Table
        public ActionResult DeleteSignature(FM_SignatoriesModel model, int SignatoriesID)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            TOSSDB.SignatoriesTables.Remove(tblSignatories);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        //Position
        #region
        //Get Add Position Partial View
        public ActionResult Get_AddPosition()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return PartialView("Position/_PositionAdd", model);
        }

        //Position Table Partial View
        public ActionResult Get_PositionTable()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            List<Signatory_PositionTable> tbl_Position = new List<Signatory_PositionTable>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Signatory_PositionTable order by PositionName asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_PositionList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Position.Add(new Signatory_PositionTable()
                        {
                            PositionID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            PositionName = GlobalFunction.ReturnEmptyString(dr[1]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getPosition = tbl_Position.ToList();
            return PartialView("Position/_PositionTable", model.getPosition);

        }

        //Get Position Update
        public ActionResult Get_UpdatePosition (FM_SignatoriesModel model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            model.getPositionColumns.PositionID = tblPosition.PositionID;
            model.getPositionColumns.PositionName = tblPosition.PositionName;
            return PartialView("Position/_PositionUpdate", model);
        }

        //Add Position
        public JsonResult AddPosition(FM_SignatoriesModel model)
        {
            Signatory_PositionTable tblPositionName = new Signatory_PositionTable();
            tblPositionName.PositionName = GlobalFunction.ReturnEmptyString(model.getPositionColumns.PositionName);
            TOSSDB.Signatory_PositionTable.Add(tblPositionName);
            TOSSDB.SaveChanges();
            return Json(tblPositionName);
        }
     
        public ActionResult UpdatePosition(FM_SignatoriesModel model)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == model.getPositionColumns.PositionID select e).FirstOrDefault();
            tblPosition.PositionName = model.getPositionColumns.PositionName;
            TOSSDB.Entry(tblPosition);
            TOSSDB.SaveChanges();
            return PartialView("Position/_PositionUpdate", model);
        }

        //Delete Position Table
        public ActionResult DeletePosition(FM_SignatoriesModel model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            TOSSDB.Signatory_PositionTable.Remove(tblPosition);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
        //Department
        #region
        //Department Table Partial View
        public ActionResult Get_DepartmentTable()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            List<Signatory_DepartmentTable> tbl_Department = new List<Signatory_DepartmentTable>();

            var SQLQuery = "SELECT * FROM DB_TOSS.dbo.Signatory_DepartmentTable order by Department asc";
            //SQLQuery += " WHERE (IsActive != 0)";
            using (SqlConnection Connection = new SqlConnection(GlobalFunction.ReturnConnectionString()))
            {
                Connection.Open();
                using (SqlCommand command = new SqlCommand("[dbo].[SP_SignatoryDepartmentList]", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@SQLStatement", SQLQuery));
                    SqlDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        tbl_Department.Add(new Signatory_DepartmentTable()
                        {
                            DepartmentID = GlobalFunction.ReturnEmptyInt(dr[0]),
                            Department = GlobalFunction.ReturnEmptyString(dr[1]),
                            DepartmentCode = GlobalFunction.ReturnEmptyString(dr[2]),
                        });
                    }
                }
                Connection.Close();
            }
            model.getDepartment = tbl_Department.ToList();
            return PartialView("Department/_DepartmentTable", model.getDepartment);
        }

        //Get Add Department Partial View
        public ActionResult Get_AddDepartment()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return PartialView("Department/_DepartmentAdd", model);
        }

        //Add Department
        public JsonResult AddDepartment(FM_SignatoriesModel model)
        {
            Signatory_DepartmentTable tblDepartment = new Signatory_DepartmentTable();
            tblDepartment.Department = GlobalFunction.ReturnEmptyString(model.getDepartmentColumns.Department);
            tblDepartment.DepartmentCode = GlobalFunction.ReturnEmptyString(model.getDepartmentColumns.DepartmentCode);
            TOSSDB.Signatory_DepartmentTable.Add(tblDepartment);
            TOSSDB.SaveChanges();
            return Json(tblDepartment);
        }

        //Get Position Update
        public ActionResult Get_UpdateDepartment(FM_SignatoriesModel model, int DepartmentID)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            model.getDepartmentColumns.DepartmentID = tblDepartment.DepartmentID;
            model.getDepartmentColumns.Department = tblDepartment.Department;
            model.getDepartmentColumns.DepartmentCode = tblDepartment.DepartmentCode;
            return PartialView("Department/_DepartmentUpdate", model);
        }

        public ActionResult UpdateDepartment(FM_SignatoriesModel model)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == model.getDepartmentColumns.DepartmentID select e).FirstOrDefault();
            tblDepartment.Department = model.getDepartmentColumns.Department;
            tblDepartment.DepartmentCode = model.getDepartmentColumns.DepartmentCode;
            TOSSDB.Entry(tblDepartment);
            TOSSDB.SaveChanges();
            return PartialView("Department/_DepartmentUpdate", model);
        }
        //Delete Position Table
        public ActionResult DeleteDepartment(FM_SignatoriesModel model, int DepartmentID)
        {
            Signatory_DepartmentTable tblDepartment = (from e in TOSSDB.Signatory_DepartmentTable where e.DepartmentID == DepartmentID select e).FirstOrDefault();
            TOSSDB.Signatory_DepartmentTable.Remove(tblDepartment);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}