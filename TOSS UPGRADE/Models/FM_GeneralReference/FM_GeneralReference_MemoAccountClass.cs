using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace TOSS_UPGRADE.Models.FM_GeneralReference
{
    public class FM_GeneralReference_MemoAccountClass
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_GeneralReference_MemoAccountClass()
        {
            //Memo
            getMemo = new List<MemoAccClassTable>();
            getMemoColumns = new MemoAccClassTable();
            
            //Account Title
            getMMAccountTitle = new List<MemoAccClass_AccountCode>();
            getMMAccountTitleColumns = new MemoAccClass_AccountCode();
            getMMAccountTitleList = new List<MMAccountTitleList>();
        }

            public List<MMAccountTitleList> getMMAccountTitleList { get; set; }

            public IEnumerable<System.Web.Mvc.SelectListItem> MMAccountTitleList { get; set; }

            public MemoAccClassTable getMemoColumns { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClassTable> getMemo { get; set; }

            public MemoAccClass_AccountCode getMMAccountTitleColumns { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClass_AccountCode> getMMAccountTitle { get; set; }
    }
    public class MMAccountTitleList
    {
        public int AccountID { get; set; }
        public string AccountCode { get; set; }
        public string AccountTitle { get; set; }
    }
}