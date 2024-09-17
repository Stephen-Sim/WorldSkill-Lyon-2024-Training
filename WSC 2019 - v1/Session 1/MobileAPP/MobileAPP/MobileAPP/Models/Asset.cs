using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAPP.Models
{
    public class Asset
    {
        public long ID { get; set; }
        public string AssetSN { get; set; }
        public string AssetName { get; set; }
        public long DepartmentLocationID { get; set; }
        public long EmployeeID { get; set; }
        public long AssetGroupID { get; set; }
        public string Description { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public long DepartmentID { get; set; }
        public long LocationID { get; set; }
        public string DepartmentName { get; set; }
    }
}
