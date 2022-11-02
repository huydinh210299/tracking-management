using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class SegmentationFilter
    {
        public SegmentationFilter()
        {
            BeginDate = DateTime.Today.Date;
            EndDate = DateTime.Today.Date;
        }

        public SegmentationFilter(DateTime beginDate, DateTime endDate)
        {
            BeginDate = beginDate.Date;
            EndDate = endDate.Date;
        }

        [FilterProperty(TargetPropertyName = "EndDate", FilterCondition = FilterCondition.GreaterOrEqual)]
        public DateTime BeginDate { get; set; }
        [FilterProperty(TargetPropertyName = "BeginDate", FilterCondition = FilterCondition.LessOrEqual)]
        public DateTime EndDate { get; set; }
        public int? UnitId { get; set; }
        public int? RouteId { get; set; }
        public int? CarId { get; set; }
    }
}
