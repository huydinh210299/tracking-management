using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Atmtechnicans = new HashSet<Atmtechnican>();
            Cars = new HashSet<Car>();
            DailyKmCars = new HashSet<DailyKmCar>();
            Devices = new HashSet<Device>();
            Drivers = new HashSet<Driver>();
            Rfids = new HashSet<Rfid>();
            SampleRoutes = new HashSet<SampleRoute>();
            Segmentations = new HashSet<Segmentation>();
            TransactionPoints = new HashSet<TransactionPoint>();
            Treasurers = new HashSet<Treasurer>();
            UserUnits = new HashSet<UserUnit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Atmtechnican> Atmtechnicans { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<DailyKmCar> DailyKmCars { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Rfid> Rfids { get; set; }
        public virtual ICollection<SampleRoute> SampleRoutes { get; set; }
        public virtual ICollection<Segmentation> Segmentations { get; set; }
        public virtual ICollection<TransactionPoint> TransactionPoints { get; set; }
        public virtual ICollection<Treasurer> Treasurers { get; set; }
        public virtual ICollection<UserUnit> UserUnits { get; set; }
    }
}
