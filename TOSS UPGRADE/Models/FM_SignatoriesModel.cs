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
            getPosition = new List<PositionName>();
            getPositionColumns = new PositionName();
            getSignatoriesTable = new List<SignatoriesTable>();
            getSignatoriesColumns = new SignatoriesTable();
            getSignatoriesList = new List<SignatoriesList>();
        }
        
        public PositionName getPositionColumns { get; set; }
        public int PositionID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> Position { get; set; }
        
        //Course Table List All Columns
        public IEnumerable<TOSS_UPGRADE.Models.PositionName> getPosition { get; set; }
       
        public SignatoriesTable getSignatoriesColumns { get; set; }
         public List<SignatoriesList> getSignatoriesList { get; set; }

        public IEnumerable<TOSS_UPGRADE.Models.SignatoriesTable> getSignatoriesTable { get; set; }
    }


    public class SignatoriesList
    {
        public int SignatoriesID { get; set; }
        
        public string SignatoriesName { get; set; }
        
        public string PositionNames { get; set; }
    }
}