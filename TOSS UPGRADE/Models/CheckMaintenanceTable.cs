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
    
    public partial class CheckMaintenanceTable
    {
        public int CheckMainteID { get; set; }
        public int BankID { get; set; }
        public int BankAccountID { get; set; }
        public int CheckInvntID { get; set; }
    
        public virtual BankAccountTable BankAccountTable { get; set; }
        public virtual BankTable BankTable { get; set; }
        public virtual CheckInventoryTable CheckInventoryTable { get; set; }
    }
}
