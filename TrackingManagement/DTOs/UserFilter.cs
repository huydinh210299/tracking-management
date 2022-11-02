using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UserFilter
    {
        public UserFilter()
        {
            this.Status = true;
        }

        #nullable enable
        public bool Status { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string? UserName { get; set; }
        public int? ScopeId { get; set; }
    }
}
