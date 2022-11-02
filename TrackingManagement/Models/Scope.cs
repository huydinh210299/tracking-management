using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Scope
    {
        public Scope()
        {
            ScopePermissions = new HashSet<ScopePermission>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string AllowedRoute { get; set; }

        public virtual ICollection<ScopePermission> ScopePermissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
