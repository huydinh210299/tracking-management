using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class DailyKmCar
    {
        public int Id { get; set; }
        public string Driver { get; set; }
        public string Treasure { get; set; }
        public double? TotalKm { get; set; }
        public DateTime? ReportTime { get; set; }
        public string UnitName { get; set; }
        public string CarLicensePlate { get; set; }
        public int? UnitId { get; set; }
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
