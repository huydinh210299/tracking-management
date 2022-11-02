using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateMember
    {
        public UpdateMember()
        {
        }
        #nullable enable
        public int Id { get; set; }
        public string? Avatar { get; set; }
        public string? EmployeeCode { get; set; }
        public string? Name { get; set; }
        public string? Sex { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool? Status { get; set; }
        public int? UnitId { get; set; }
        public int? Rfidid { get; set; }
    }
}
