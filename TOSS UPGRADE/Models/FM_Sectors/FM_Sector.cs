using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TOSS_UPGRADE.Models.FM_Sectors
{
    public class FM_Sector
    {
        DB_TOSSEntities db = new DB_TOSSEntities();
        public FM_Sector()
        {
            getSector = new List<Sector>();
            getSectorcolumns = new Sector();
            getSectorList = new List<SectorList>();


            getSubSector = new List<SubSector>();
            getSubSectorcolumns = new SubSector();
            getSubSectorList = new List<SubSectorList>();
        }
        public List<SectorList> getSectorList { get; set; }
        public Sector getSectorcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SectorList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.Sector> getSector { get; set; }

        public List<SubSectorList> getSubSectorList { get; set; }
        public SubSector getSubSectorcolumns { get; set; }
        public IEnumerable<System.Web.Mvc.SelectListItem> SubSectorList { get; set; }
        public IEnumerable<TOSS_UPGRADE.Models.SubSector> getSubSector { get; set; }
        public int SubSectorNameID { get; set; }
        public int SubSectorNameTempID { get; set; }
    }
    public class SectorList
    {
        public int SectorID { get; set; }
        public string SectorName { get; set; }
        public string SectorCode { get; set; }
    }

    public class SubSectorList
    {
        public int SubSectorID { get; set; }
        public string SectorName { get; set; }
        public string SubSectorName { get; set; }
        public string SubSectorCode { get; set; }
    }
}