using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Car
    {
        public Car()
        {
            DailyCars = new HashSet<DailyCar>();
            DailyKmCars = new HashSet<DailyKmCar>();
            Devices = new HashSet<Device>();
            Histories = new HashSet<History>();
            Onlines = new HashSet<Online>();
            Segmentations = new HashSet<Segmentation>();
        }

        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public int NumberCamera { get; set; }
        public int? FirstCamPo { get; set; }
        public int? FirstCamRotation { get; set; }
        public int? SecondCamRotation { get; set; }
        public int? Fuel { get; set; }
        public int? LimitedSpeed { get; set; }
        public int? UnitId { get; set; }
        public int? RfidId { get; set; }
        public int? DriverId { get; set; }

        public virtual Driver Driver { get; set; }
        public virtual Rfid Rfid { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<DailyCar> DailyCars { get; set; }
        public virtual ICollection<DailyKmCar> DailyKmCars { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Online> Onlines { get; set; }
        public virtual ICollection<Segmentation> Segmentations { get; set; }
    }
}
