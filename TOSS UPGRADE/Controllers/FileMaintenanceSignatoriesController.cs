﻿using System;
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

            model.Position = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName = s.PositionName }), "PositionID", "PositionName");

            return PartialView("_DynamicDDPositionName", model);
        }

        //Position in Dropdown
        public ActionResult GetDynamicDepartment()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();

            model.Department = new SelectList((from s in TOSSDB.Signatory_DepartmentTable.ToList() orderby s.Department ascending select new { DepartmentID = s.DepartmentID, Department = s.Department }), "DepartmentID", "Department");

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
                            PositionNames = GlobalFunction.ReturnEmptyString(dr[4]),
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
        
        public ActionResult GetDynamicSignatories2(int SignatoriesID)
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();

            model.Position = new SelectList((from s in TOSSDB.Signatory_PositionTable.ToList() orderby s.PositionName ascending select new { PositionID = s.PositionID, PositionName1 = s.PositionName }), "PositionID", "PositionName");
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            model.PositionID = tblSignatories.PositionID;
            return PartialView("_DynamicDDPositionName", model);
        }

        //Get Signature Data
        public ActionResult Get_UpdateSignatories(FM_SignatoriesModel model, int SignatoriesID)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == SignatoriesID select e).FirstOrDefault();
            model.getSignatoriesColumns.SignatoriesID = tblSignatories.SignatoriesID;
            model.getSignatoriesColumns.SignatoriesName = tblSignatories.SignatoriesName;
            return PartialView("_SignatoriesUpdate", model);
        }

        //Update Signature Data
        public ActionResult UpdateSignatories(FM_SignatoriesModel model)
        {
            SignatoriesTable tblSignatories = (from e in TOSSDB.SignatoriesTables where e.SignatoriesID == model.getSignatoriesColumns.SignatoriesID select e).FirstOrDefault();
            tblSignatories.PositionID = model.PositionID;
            tblSignatories.SignatoriesName = model.getSignatoriesColumns.SignatoriesName;
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
        
        //Get Add Position Partial View
        public ActionResult Get_AddPosition()
        {
            FM_SignatoriesModel model = new FM_SignatoriesModel();
            return PartialView("_PositionAdd", model);
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
            return PartialView("_PositionTable", model.getPosition);

        }

        //Get Position Update
        public ActionResult Get_UpdatePosition (FM_SignatoriesModel model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            model.getPositionColumns.PositionID = tblPosition.PositionID;
            model.getPositionColumns.PositionName = tblPosition.PositionName;
            return PartialView("_PositionUpdate", model);
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
            return PartialView("_PositionUpdate", model);
        }

        //Delete Position Table
        public ActionResult DeletePosition(FM_SignatoriesModel model, int PositionID)
        {
            Signatory_PositionTable tblPosition = (from e in TOSSDB.Signatory_PositionTable where e.PositionID == PositionID select e).FirstOrDefault();
            TOSSDB.Signatory_PositionTable.Remove(tblPosition);
            TOSSDB.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}