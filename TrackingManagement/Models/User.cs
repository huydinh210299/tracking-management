using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class User
    {
        public User()
        {
            UserUnits = new HashSet<UserUnit>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public int? ScopeId { get; set; }

        public virtual Scope Scope { get; set; }
        public virtual ICollection<UserUnit> UserUnits { get; set; }
    }
}
