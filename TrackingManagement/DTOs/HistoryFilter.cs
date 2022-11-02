using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class HistoryFilter
    {
        public int CarId { get; set; }
        [FilterProperty(TargetPropertyName = "DeviceTime", FilterCondition = FilterCondition.GreaterOrEqual)]
        public DateTime BeginTime { get; set; }
        [FilterProperty(TargetPropertyName = "DeviceTime", FilterCondition = FilterCondition.LessOrEqual)]
        public DateTime EndTime { get; set; }
    }
}
