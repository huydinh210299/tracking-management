using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.Models;

namespace TrackingManagement.DTOs
{
    public class Route
    {
        public SampleRoute RouteInfo { get; set; }
        public List<RouteItem> WayPoints { get; set; }
    }
}
