using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class RouteFilter
    {
        #nullable enable
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? RouteCode { get; set; }
        public int? UnitId { get; set; }
        public int? Type { get; set; }
        public int? Id { get; set; }
    }
}
