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
    
    public partial class CheckInventoryTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckInventoryTable()
        {
            this.CheckMaintenanceTables = new HashSet<CheckMaintenanceTable>();
        }
    
        public int CheckInvntID { get; set; }
        public int BankID { get; set; }
        public int BankAccountID { get; set; }
        public int Quantity { get; set; }
        public int StartingChckNo { get; set; }
        public int EndingChckNo { get; set; }
        public System.DateTime Date { get; set; }
        public bool IsIssued { get; set; }
    
        public virtual BankAccountTable BankAccountTable { get; set; }
        public virtual BankTable BankTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckMaintenanceTable> CheckMaintenanceTables { get; set; }
    }
}
