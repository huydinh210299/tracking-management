using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.Models;

namespace TrackingManagement.DTOs
{
    public class HistoryAndKmCar
    {
        public List<History> Histories { get; set; }
        public KmCar Statistical { get; set; }
    }
}
