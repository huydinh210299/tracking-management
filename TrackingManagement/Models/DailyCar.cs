using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class DailyCar
    {
        public int Id { get; set; }
        public double? TotalKm { get; set; }
        public int? TotalFuel { get; set; }
        public int? RunningTime { get; set; }
        public int? OpenSafeBox { get; set; }
        public int? Conflict { get; set; }
        public int? SegmentationId { get; set; }
        public int? RouteDeviation { get; set; }
        public int? TimeDeviation { get; set; }
        public DateTime? ReportTime { get; set; }
        public string CarLicensePlate { get; set; }
        public string RouteCode { get; set; }
        public int? CarId { get; set; }
        public int? RouteId { get; set; }

        public virtual Car Car { get; set; }
        public virtual SampleRoute Route { get; set; }
    }
}
