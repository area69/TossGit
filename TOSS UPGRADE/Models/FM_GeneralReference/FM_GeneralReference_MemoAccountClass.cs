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
            getMemoAccList = new List<MemoAccList>();
            
            //Account Title
            getMMAccountTitle = new List<MemoAccClass_AccountCode>();
            getMMAccountTitleColumns = new MemoAccClass_AccountCode();
            getMMAccountTitleList = new List<MMAccountTitleList>();

            //Revision Year
            getMMRevisionYr = new List<MemoAccClass_RevisionYr>();
            getMMRevisionYrColumns = new MemoAccClass_RevisionYr();
            getMMRevisionYrList = new List<MMRevisionYearList>();
        }
            //Memo Account
            public MemoAccClassTable getMemoColumns { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClassTable> getMemo { get; set; }

            public List<MemoAccList> getMemoAccList { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClassTable> MemoAccList { get; set; }

        //Memo Account Classification > Account Title
            public int MMAccountTitleID { get; set; }
            public int MMAccountTitleTempID { get; set; }
            public List<MMAccountTitleList> getMMAccountTitleList { get; set; }
            public IEnumerable<System.Web.Mvc.SelectListItem> MMAccountTitleList { get; set; }

            public MemoAccClass_AccountCode getMMAccountTitleColumns { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClass_AccountCode> getMMAccountTitle { get; set; }

        //Memo Account Classification > Revision Year
            public int MMRevisionYrID { get; set; }
            public int MMRevisionYrTempID { get; set; }
            public List<MMRevisionYearList> getMMRevisionYrList { get; set; }
            public IEnumerable<System.Web.Mvc.SelectListItem> MMRevisionYearList { get; set; }
            public MemoAccClass_RevisionYr getMMRevisionYrColumns { get; set; }
            public IEnumerable<TOSS_UPGRADE.Models.MemoAccClass_RevisionYr> getMMRevisionYr { get; set; }


    }
    //  public class MMAccountTitleList
    public class MemoAccList
    {
        public int MMAccountID { get; set; }
        public string AccountTitle { get; set; }
        public string AccountCode { get; set; }

    }

    // Account Title
    public class MMAccountTitleList
    {
        public int AccountID { get; set; }
        public string AccountCode { get; set; }
        public string AccountTitle { get; set; }
    }

    //Revision Year
    public class MMRevisionYearList
    {
        public int RevisionID { get; set; }
        public int RevisionYear { get; set; }
    }
}