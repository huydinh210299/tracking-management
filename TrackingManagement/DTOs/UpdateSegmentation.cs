using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateSegmentation
    {
        #nullable enable
        public TimeSpan? BeginTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Day { get; set; }
        public bool? Control { get; set; }
        public bool? Sms { get; set; }
        public int? UnitId { get; set; }
        public int? CarId { get; set; }
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public int? TreasurerId { get; set; }
        public int? AtmtechnicanId { get; set; }
    }
}
