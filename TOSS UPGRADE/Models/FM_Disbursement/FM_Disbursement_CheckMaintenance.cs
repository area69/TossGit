using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Disbursement
{
    public class FM_Disbursement_CheckMaintenance
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Disbursement_CheckMaintenance()
        {
            getCheckMaintenanceType = new List<CheckMaintenanceTable>();
            getCheckMaintenancecolumns = new CheckMaintenanceTable();
            getCheckMaintenanceList = new List<CheckMaintenanceList>();
        }
    public List<CheckMaintenanceList> getCheckMaintenanceList { get; set; }
    public CheckMaintenanceTable getCheckMaintenancecolumns { get; set; }
    public IEnumerable<System.Web.Mvc.SelectListItem> CheckMaintenanceList { get; set; }
    public IEnumerable<TOSS_UPGRADE.Models.CheckMaintenanceTable> getCheckMaintenanceType { get; set; }
    public int CheckMaintenanceBankTempID { get; set; }

    public IEnumerable<System.Web.Mvc.SelectListItem> CheckMaintenanceBankList
    {
        get
        {
            List<BankTable> AccountNameLists = db.BankTables.ToList();
            return new System.Web.Mvc.SelectList(AccountNameLists, "BankID", "BankName");
        }
    }

        public IEnumerable<System.Web.Mvc.SelectListItem> CheckMaintenanceInventoryList { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> CheckMaintenanceAccountNameList { get; set; }

        public int CheckMaintenanceAccountNameID { get; set; }

        public int CheckMaintenanceInventoryID { get; set; }

        public int CheckMaintenanceQuantityID { get; set; }

        public int CheckMaintenanceBankID { get; set; }
        public int CheckMaintenanceBankIDTemp { get; set; }

        public int CheckMaintenanceStartingNoID { get; set; }
        public int CheckMaintenanceEndingNoID { get; set; }
        public int CheckMaintenanceAccountNameTempID { get; set; }
    }
    public class CheckMaintenanceList
    {
        public int CheckMainteID { get; set; }
        public string Bank { get; set; }
        public string AccountName { get; set; }
        public int Quantity { get; set; }
        public int StartingChckNo { get; set; }
        public int EndingChckNo { get; set; }
    }
}