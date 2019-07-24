using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models
{
    public class FM_SignatoriesModel
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_SignatoriesModel()
        {
            getPosition = new List<Signatory_PositionTable>();
            getPositionColumns = new Signatory_PositionTable();

            getSignatoriesTable = new List<SignatoriesTable>();
            getSignatoriesColumns = new SignatoriesTable();
            getSignatoriesList = new List<SignatoriesList>();

            getDepartment = new List<Signatory_DepartmentTable>();
            getDepartmentColumns = new Signatory_DepartmentTable();
        }
        
        public Signatory_PositionTable getPositionColumns { get; set; }
        public int PositionID { get; set; }
        public int PositionTempID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Position { get; set; }
        
        //Course Table List All Columns
        public IEnumerable<TOSS_UPGRADE.Models.Signatory_PositionTable> getPosition { get; set; }
       
        public SignatoriesTable getSignatoriesColumns { get; set; }
         public List<SignatoriesList> getSignatoriesList { get; set; }

        public IEnumerable<TOSS_UPGRADE.Models.SignatoriesTable> getSignatoriesTable { get; set; }


        public Signatory_DepartmentTable getDepartmentColumns { get; set; }
        public int DepartmentID { get; set; }
        public int DepartmentTempID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Department { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Signatory_DepartmentTable> getDepartment { get; set; }

        public bool isDeptHeads { get; set; }
    }


    public class SignatoriesList
    {
        public int SignatoriesID { get; set; }
        
        public string SignatoriesName { get; set; }
        
        public string PositionNames { get; set; }

        public string Department { get; set; }
        public string DepartmentCode  { get; set; }

        public bool idDeptHead { get; set; }
    }
}