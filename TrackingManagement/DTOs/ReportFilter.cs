using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class ReportFilter
    {
        [FilterProperty(TargetPropertyName = "ReportTime", FilterCondition = FilterCondition.GreaterOrEqual)]
        public DateTime BeginDate { get; set; }
        [FilterProperty(TargetPropertyName = "ReportTime", FilterCondition = FilterCondition.LessOrEqual)]
        public DateTime EndDate { get; set; }
        public int UnitId { get; set; }
        public List<int> ObjectList { get; set; }
        [NotAutoFiltered]
        public int Type { get; set; }

    }
}
