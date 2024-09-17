using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAPP.Models
{
    public class StoreAssetRequest
    {
        public string AssetName { get; set; }
        public string AssetSN { get; set; }
        public long DepartmentId { get; set; }
        public long LocationId { get; set; }
        public long AssetGroupId { get; set; }
        public long EmployeeId { get; set; }
        public string AssetDesc { get; set; }
        
        public DateTime? ExipredDate { get; set; }
        public List<byte[]> AssetImages { get; set; } = new List<byte[]>();
    }

    public class EditAssetRequest
    {
        public long AssetID { get; set; }
        public string AssetName { get; set; }
        public long EmployeeId { get; set; }
        public string AssetDesc { get; set; }

        public DateTime? ExipredDate { get; set; }
        public List<byte[]> AssetImages { get; set; } = new List<byte[]>();
    }

    public class TransferAssetRequest
    {
        public long AssetID { get; set; }
        public long DepartmentId { get; set; }
        public long LocationId { get; set; }
        public string NewAssetSN { get; set; }
    }
}
