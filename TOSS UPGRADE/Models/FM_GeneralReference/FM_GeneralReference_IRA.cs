using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_GeneralReference
{
    public class FM_GeneralReference_IRA
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_GeneralReference_IRA()
        {
            getIRA = new List<IRA_Table>();
            getIRAcolumns = new IRA_Table();
            getIRAList = new List<IRAList>();
        }
        public List<IRAList> getIRAList { get; set; }
        public IRA_Table getIRAcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> IRAList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.IRA_Table> getIRA { get; set; }

    }
    public class IRAList
    {
        public int IRAID { get; set; }
        public decimal IRAPercentageShare { get; set; }
        public int IRAPercent { get; set; }
        public decimal IRABase { get; set; }
    }
}
