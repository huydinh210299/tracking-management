using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class UserUnit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UnitId { get; set; }

        public virtual Unit Unit { get; set; }
        public virtual User User { get; set; }
    }
}
