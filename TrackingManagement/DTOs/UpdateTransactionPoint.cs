using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateTransactionPoint
    {
        #nullable enable
        public string? PointCode { get; set; }
        public string? PointName { get; set; }
        public int? PointType { get; set; }
        public string? Address { get; set; }
        public double? Longtitude { get; set; }
        public double? Latitude { get; set; }
        public string? Branch { get; set; }
        public string? Contact { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public int? UnitId { get; set; }
    }
}
