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
    
    public partial class AccountableForm_Assignment
    {
        public int AssignAFID { get; set; }
        public int CollectorID { get; set; }
        public int FundID { get; set; }
        public int AFORID { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual AccountableForm_Inventory AccountableForm_Inventory { get; set; }
        public virtual CollectorTable CollectorTable { get; set; }
        public virtual FundType_FundName FundType_FundName { get; set; }
    }
}
