using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_CollectionAndDeposit
{
    public class FM_CollectionAndDeposit_InventoryAF
    {
        public FM_CollectionAndDeposit_InventoryAF()
        {
            getAccountableFormInvt = new List<AccountableForm_Inventory>();
            getAccountableFormInvtcolumns = new AccountableForm_Inventory();
            getAccountableFormInvtList = new List<AccountableFormInvtList>();
        }
        public List<AccountableFormInvtList> getAccountableFormInvtList { get; set; }
        public AccountableForm_Inventory getAccountableFormInvtcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> AccountableFormInvtList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.AccountableForm_Inventory> getAccountableFormInvt { get; set; }
        public int AccountableFormInvtID { get; set; }
    }
    public class AccountableFormInvtList
    {
        public int AFORID { get; set; }
        public string AF { get; set; }
        public int StubNo { get; set; }
        public int Quantity { get; set; }
        public int StratingOR { get; set; }
        public int EndingOR { get; set; }
        public string Date { get; set; }
    }
}