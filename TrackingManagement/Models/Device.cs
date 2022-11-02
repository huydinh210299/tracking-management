using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Device
    {
        public int Id { get; set; }
        public string DeviceNumber { get; set; }
        public string Imei { get; set; }
        public string Phone { get; set; }
        public int MobileCarrier { get; set; }
        public DateTime? ActivationTime { get; set; }
        public bool? Status { get; set; }
        public bool? AllowUpdate { get; set; }
        public int? UnitId { get; set; }
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
        public virtual Unit Unit { get; set; }
    }
}
