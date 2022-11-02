using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class PaginationFilter
    {
        public int Page { get; set; }
        public int Record { get; set; }
        public bool Paging { get; set; }
        public PaginationFilter()
        {
            Page = 1;
            Record = 10;
            Paging = true;
        }

        public PaginationFilter(int page, int record)
        {
            this.Page = page < 1 ? 1 : page;
            this.Record = record > 10 ? 10 : record;
        }
    }
}
