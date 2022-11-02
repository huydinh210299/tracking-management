using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateCar
    {
        public UpdateCar()
        {
        }
        #nullable enable
        public int Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? Type { get; set; }
        public int NumberCamera { get; set; }
        public int? FirstCamPo { get; set; }
        public int? FirstCamRotation { get; set; }
        public int? SecondCamRotation { get; set; }
        public int? Fuel { get; set; }
        public int? LimitedSpeed { get; set; }
        public int? UnitId { get; set; }
        public int? RfidId { get; set; }
        public int? DriverId { get; set; }
    }
}
