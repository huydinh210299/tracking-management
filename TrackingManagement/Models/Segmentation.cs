using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Segmentation
    {
        public Segmentation()
        {
            EditedSegmentationRoutes = new HashSet<EditedSegmentationRoute>();
        }

        public int Id { get; set; }
        public TimeSpan BeginTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Day { get; set; }
        public bool? Control { get; set; }
        public bool? Sms { get; set; }
        public int? UnitId { get; set; }
        public int? CarId { get; set; }
        public int? DriverId { get; set; }
        public int? RouteId { get; set; }
        public int? TreasurerId { get; set; }
        public int? AtmtechnicanId { get; set; }

        public virtual Atmtechnican Atmtechnican { get; set; }
        public virtual Car Car { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual SampleRoute Route { get; set; }
        public virtual Treasurer Treasurer { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<EditedSegmentationRoute> EditedSegmentationRoutes { get; set; }
    }
}
