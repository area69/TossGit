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
    
    public partial class BankTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BankTable()
        {
            this.BankAccountTables = new HashSet<BankAccountTable>();
            this.CheckInventoryTables = new HashSet<CheckInventoryTable>();
        }
    
        public int BankID { get; set; }
        public string BankName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankAccountTable> BankAccountTables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CheckInventoryTable> CheckInventoryTables { get; set; }
    }
}
