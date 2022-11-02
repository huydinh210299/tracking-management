using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class CreateReport
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Obj { get; set; }
        public int Type { get; set; }
        public string ReportBody { get; set; }
    }
}
