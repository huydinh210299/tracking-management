using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class TransactionPoint
    {
        public int Id { get; set; }
        public string PointCode { get; set; }
        public string PointName { get; set; }
        public int PointType { get; set; }
        public string Address { get; set; }
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Branch { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public int? UnitId { get; set; }

        public virtual Unit Unit { get; set; }
    }
}
