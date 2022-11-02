using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class RfidFilter
    {
        public RfidFilter(){
        }
        #nullable enable
        public int? Id { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? CardNumber { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Description { get; set; }
        public int? Type { get; set; }
        public DateTime? ActivationTime { get; set; }
        public int? UnitId { get; set; }
        public bool? Status { get; set; }
        public bool? IsDistributed { get; set; }
    }
}
