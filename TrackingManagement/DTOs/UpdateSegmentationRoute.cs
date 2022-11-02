using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateSegmentationRoute
    {
        public int SegmentationId { get; set; }
        public string Route { get; set; }
        public string Direction { get; set; }
        public string WayBack { get; set; }
        public DateTime EditedIn { get; set; }
        public bool AutoTurnBack { get; set; }
    }
}
