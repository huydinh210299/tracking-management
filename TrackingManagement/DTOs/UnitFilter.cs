using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UnitFilter
    {
        #nullable enable
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? Name { get; set; }
    }
}
