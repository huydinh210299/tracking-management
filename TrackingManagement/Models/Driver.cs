using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Driver
    {
        public Driver()
        {
            Cars = new HashSet<Car>();
            Segmentations = new HashSet<Segmentation>();
        }

        public int Id { get; set; }
        public string Avatar { get; set; }
        public string EmployeeCode { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
        public int? UnitId { get; set; }
        public int? Rfidid { get; set; }

        public virtual Rfid Rfid { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Segmentation> Segmentations { get; set; }
    }
}
