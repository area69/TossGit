//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOSS_UPGRADE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BankAccountTable
    {
        public int BankAccountID { get; set; }
        public int BankID { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public int FundID { get; set; }
        public string CurrentAccount { get; set; }
        public int AccountCodeID { get; set; }
        public int AccountTypeID { get; set; }
    
        public virtual BankAccount_AccountCode BankAccount_AccountCode { get; set; }
        public virtual BankAccount_AccountType BankAccount_AccountType { get; set; }
        public virtual BankTable BankTable { get; set; }
        public virtual FundType_FundName FundType_FundName { get; set; }
    }
}
