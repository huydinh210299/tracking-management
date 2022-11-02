using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Online
    {
        public int Id { get; set; }
        public string Cam1ImgPath { get; set; }
        public string Cam2ImgPath { get; set; }
        public string Rfidstring { get; set; }
        public string AppVersion { get; set; }
        public DateTime? ReceivedTime { get; set; }
        public DateTime? DeviceTime { get; set; }
        public int? EngineOn { get; set; }
        public bool? StrongBoxOpen { get; set; }
        public bool? IsSos { get; set; }
        public double? GpsLat { get; set; }
        public double? GpsLon { get; set; }
        public double? NetworkLat { get; set; }
        public double? NetworkLon { get; set; }
        public int? GpsVelocity { get; set; }
        public int? CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
