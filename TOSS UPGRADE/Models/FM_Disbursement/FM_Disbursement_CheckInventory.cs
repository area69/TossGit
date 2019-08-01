using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Disbursement
{
    public class FM_Disbursement_CheckInventory
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Disbursement_CheckInventory()
        {
            getCheckInventoryType = new List<CheckInventoryTable>();
            getCheckInventorycolumns = new CheckInventoryTable();
            getCheckInventoryList = new List<CheckInventoryList>();
        }
        public List<CheckInventoryList> getCheckInventoryList { get; set; }
        public CheckInventoryTable getCheckInventorycolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CheckInventoryList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.CheckInventoryTable> getCheckInventoryType { get; set; }
        public int CheckInventoryBankTempID { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> CheckInventoryBankList
        {
            get
            {
                List<BankTable> AccountNameLists = db.BankTables.ToList();
                return new System.Web.Mvc.SelectList(AccountNameLists, "BankID", "BankName");
            }
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> CheckInventoryAccountNameList { get; set; }
        public int CheckInventoryAccountNameID { get; set; }
        public int CheckInventoryAccountNameTempID { get; set; }
    }
    public class CheckInventoryList
    {
        public int CheckInvntID { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public int Quantity { get; set; }
        public int StartingChckNo { get; set; }
        public int EndingChckNo { get; set; }
        public DateTime Date { get; set; }
    }
}