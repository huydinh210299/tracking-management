using AutoFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class TransactionPointFilter
    {
        public TransactionPointFilter()
        {
        }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string PointCode { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string PointName { get; set; }
        [FilterProperty(StringFilter = StringFilterCondition.Contains, IgnoreCase = true)]
        public string Address { get; set; }
    }
}
