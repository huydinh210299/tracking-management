using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class KmCar
    {
        public Double AverageSpeed { get; set; }
        public int MinimumSpeed { get; set; }
        public int MaximumSpeed { get; set; }
        public Double Distance { get; set; }
    }
}
