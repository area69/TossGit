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
    
    public partial class ParentModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParentModule()
        {
            this.Level1Modules = new HashSet<Level1Modules>();
        }
    
        public int ModuleParentID { get; set; }
        public string ModuleName { get; set; }
        public string icon { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string JSID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Level1Modules> Level1Modules { get; set; }
        public virtual ParentModule ParentModules1 { get; set; }
        public virtual ParentModule ParentModule1 { get; set; }
    }
}
