using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Funds
{
    public class FM_Fund
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Fund()
        {
            getFund = new List<Fund>();
            getFundcolumns = new Fund();
            getFundList = new List<FundList>();


            getSubFund = new List<SubFund>();
            getSubFundcolumns = new SubFund();
            getSubFundList = new List<SubFundList>();
        }
        public List<FundList> getFundList { get; set; }
        public Fund getFundcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FundList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Fund> getFund { get; set; }

        public List<SubFundList> getSubFundList { get; set; }
        public SubFund getSubFundcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SubFundList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.SubFund> getSubFund { get; set; }
        public int SubFundNameID { get; set; }
        public int SubFundNameTempID { get; set; }
    }
    public class FundList
    {
        public int FundID { get; set; }
        public string FundName { get; set; }
        public string FundCode { get; set; }
    }

    public class SubFundList
    {
        public int SubFundID { get; set; }
        public string FundName { get; set; }
        public string SubFundName { get; set; }
    }
}