using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Rfid
    {
        public Rfid()
        {
            Atmtechnicans = new HashSet<Atmtechnican>();
            Cars = new HashSet<Car>();
            Drivers = new HashSet<Driver>();
            Treasurers = new HashSet<Treasurer>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Description { get; set; }
        public DateTime? ActivationTime { get; set; }
        public int Type { get; set; }
        public bool IsDistributed { get; set; }
        public bool? Status { get; set; }
        public int? UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual ICollection<Atmtechnican> Atmtechnicans { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Driver> Drivers { get; set; }
        public virtual ICollection<Treasurer> Treasurers { get; set; }
    }
}
