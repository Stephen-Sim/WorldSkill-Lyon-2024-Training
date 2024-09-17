using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAPP.Models
{
    public class TransferLog
    {
        public string TransferDate { get; set; }
        public string OldDepartment { get; set; }
        public string NewDepartment { get; set; }
        public string OldAssetSN { get; set; }
        public string NewAssetSN { get; set; }
    }
}
