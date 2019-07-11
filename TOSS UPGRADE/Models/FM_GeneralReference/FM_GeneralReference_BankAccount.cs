using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models
{
    public class FM_GeneralReference_BankAccount
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_GeneralReference_BankAccount()
        {
            //Bank Name
            getBankAccountBank = new List<BankTable>();
            getBankAccountBankColumns = new BankTable();
            getBankAccountBankList = new List<BankAccountBankList>();
            //Fund Type
            getFundType = new List<FundType_FundName>();
            getFundTypeColumns = new FundType_FundName();
            //Account Code
            getAccountCode = new List<BankAccount_AccountCode>();
            getAccountCodeColumns = new BankAccount_AccountCode();
            //Account Type
            getAccountType = new List<BankAccount_AccountType>();
            getAccountTypeColumns = new BankAccount_AccountType();
        }
        //Bank Account Bank Name
        public BankTable getBankAccountBankColumns { get; set; }
        public List<BankAccountBankList> getBankAccountBankList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankTable> getBankAccountBank { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BankAccountBankList { get; set; }
        public int BankAccountBankID { get; set; }
        //Fund Type
        public FundType_FundName getFundTypeColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.FundType_FundName> getFundType { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BankAccountFundTypeList { get; set; }
        public int FundID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> FundNameList
        {
            get
            {
                List<FundType_FundName> FundNameLists = db.FundType_FundName.ToList();
                return new System.Web.Mvc.SelectList(FundNameLists, "FundID", "FundTitle");
            }
        }

        //Bank Account Account Code
        public BankAccount_AccountCode getAccountCodeColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankAccount_AccountCode> getAccountCode { get; set; }
        public int AccountCodeID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> getBankAccountAccountCodeList { get; set; }
        public List<BankAccountAccountCodeList> getBankAccountAccountCode { get; set; }

        //Account Type
        public BankAccount_AccountType getAccountTypeColumns { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.BankAccount_AccountType> getAccountType { get; set; }
        public int AccountTypeID { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> getBankAccountAccountTypeList { get; set; }
        public List<BankAccountAccountTypeList> getBankAccountAccountType { get; set; }
    }
    //Bank Name
    public class BankAccountBankList
    {
        public int BankAccountBankID { get; set; }
        public string BankName { get; set; }
    }    
    //Account Code
    public class BankAccountAccountCodeList
    {
        public int BankAccountAccountCodeID { get; set; }
        public string BankAccountAccountCode { get; set; }
    }
    //Account Type
    public class BankAccountAccountTypeList
    {
        public int BankAccountAccountTypeID { get; set; }
        public string BankAccountAccountType { get; set; }
    }
}