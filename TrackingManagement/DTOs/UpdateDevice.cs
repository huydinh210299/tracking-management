using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateDevice
    {
        #nullable enable
        public string? DeviceNumber { get; set; }
        public string? Imei { get; set; }
        public string? Phone { get; set; }
        public int? MobileCarrier { get; set; }
        public DateTime? ActivationTime { get; set; }
        public bool? Status { get; set; }
        public bool? AllowUpdate { get; set; }
        public int? UnitId { get; set; }
        public int? CarId { get; set; }
    }
}
