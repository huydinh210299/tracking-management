using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class MemberFilter
    {
#nullable enable
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? EmployeeCode { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Name { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Sex { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Phone { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public int? UnitId { get; set; }
    }
}
