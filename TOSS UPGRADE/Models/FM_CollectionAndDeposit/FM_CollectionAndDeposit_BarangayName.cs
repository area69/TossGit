using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_CollectionAndDeposit
{
    public class FM_CollectionAndDeposit_BarangayName
    {
        public FM_CollectionAndDeposit_BarangayName()
        {
            getBarangayName = new List<Barangay_BarangayName>();
            getBarangayNamecolumns = new Barangay_BarangayName();
            getBarangayNameList = new List<BarangayNameList>();
        }
        public List<BarangayNameList> getBarangayNameList { get; set; }
        public Barangay_BarangayName getBarangayNamecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> BarangayNameList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Barangay_BarangayName> getBarangayName { get; set; }
    }
    public class BarangayNameList
    {
        public int BarangayID { get; set; }
        public string BarangayName { get; set; }
    }
}