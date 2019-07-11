using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models
{
    public class FM_GeneralReference_FundTypes
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_GeneralReference_FundTypes()
        {
            getFund = new List<FundType_FundTypeTable>();
            getFundColumns = new FundType_FundTypeTable();
            getFundList = new List<FundList>();
        }

        public List<FundList> getFundList { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FundTypeList { get; set; }
        public FundType_FundTypeTable getFundColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.FundType_FundTypeTable> getFund { get; set; }

      
        public int FundID { get; set; }
        public int ListFundTypeID { get; set; }
        public int FundTypeID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FundNameList
        {
            get
            {
                List<FundType_FundName> FundNameLists = db.FundType_FundName.ToList();
                return new System.Web.Mvc.SelectList(FundNameLists, "FundID", "FundTitle");
            }
        }
    }

    public class FundList
    {
        public int ListFundTypeID { get; set; }
        public string FundTitle { get; set; }
        public string FundTypeTitle { get; set; }
        public string Particulars { get; set; }
    }
}