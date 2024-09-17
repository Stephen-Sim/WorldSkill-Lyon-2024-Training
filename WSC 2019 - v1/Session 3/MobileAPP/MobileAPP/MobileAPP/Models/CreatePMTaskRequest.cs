using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAPP.Models
{
    public class CreatePMTaskRequest
    {
        public long TaskId { get; set; }
        public long ModelId { get; set; }
        public List<long> AssetIds { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? DailyInterval { get; set; }
        public DayOfWeek? DayOfWeekInterval { get; set; }
        public int? MonthlyInterval { get; set; }

        public int? StartMilage { get; set; }
        public int? EndMilage { get; set; }
        public int? MilageInteval { get; set; }
    }
}
