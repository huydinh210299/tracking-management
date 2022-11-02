using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class EditedSegmentationRoute
    {
        public int Id { get; set; }
        public string Route { get; set; }
        public string Direction { get; set; }
        public string WayBack { get; set; }
        public bool AutoTurnBack { get; set; }
        public DateTime EditedIn { get; set; }
        public int? SegmentationId { get; set; }

        public virtual Segmentation Segmentation { get; set; }
    }
}
