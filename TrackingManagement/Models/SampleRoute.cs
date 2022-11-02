using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class SampleRoute
    {
        public SampleRoute()
        {
            DailyCars = new HashSet<DailyCar>();
            Segmentations = new HashSet<Segmentation>();
        }

        public int Id { get; set; }
        public string RouteCode { get; set; }
        public int Type { get; set; }
        public string Route { get; set; }
        public string Direction { get; set; }
        public string WayBack { get; set; }
        public bool AutoTurnBack { get; set; }
        public TimeSpan BeginTime { get; set; }
        public int OverTimeAllowed { get; set; }
        public int ToleranceAllowed { get; set; }
        public double Distance { get; set; }
        public int ArrivalTime { get; set; }
        public bool? Permanent { get; set; }
        public int? UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<DailyCar> DailyCars { get; set; }
        public virtual ICollection<Segmentation> Segmentations { get; set; }
    }
}
