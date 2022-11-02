using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class DeviceFilter
    {
        #nullable enable
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? DeviceNumber { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Imei { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Phone { get; set; }
        public int? MobileCarrier { get; set; }
        public int? UnitId { get; set; }
    }
}
