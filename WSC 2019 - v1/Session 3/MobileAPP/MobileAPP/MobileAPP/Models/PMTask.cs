using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAPP.Models
{
    public class PMTask
    {
        public long ID { get; set; }
        public string AssetInfo { get; set; }
        public string TaskName { get; set; }
        public bool TaskDone { get; set; }
        public long TaskID { get; set; }
        public long AssetID { get; set; }
        public string DisplayTaskTypeInfo { get; set; }
        public string Color { get; set; }
    }
}
