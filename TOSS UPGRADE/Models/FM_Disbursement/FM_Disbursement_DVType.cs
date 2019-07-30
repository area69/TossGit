using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TOSS_UPGRADE.Models.FM_Disbursement
{
    public class FM_Disbursement_DVType
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Disbursement_DVType()
        {
            getDVType = new List<DVTypeTable>();
            getDVTypecolumns = new DVTypeTable();
            getDVTypeList = new List<DVTypeList>();
        }
         public List<DVTypeList> getDVTypeList { get; set; }
        public DVTypeTable getDVTypecolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> DVTypeList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.DVTypeTable> getDVType { get; set; }
    }
    public class DVTypeList
    {
        public int DVTypeID { get; set; }
        public string DVTypeName { get; set; }
    }
}