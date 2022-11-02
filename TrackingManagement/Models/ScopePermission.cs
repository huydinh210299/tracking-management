using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class ScopePermission
    {
        public int Id { get; set; }
        public string Allowed { get; set; }
        public string Filter { get; set; }
        public int? PermissionId { get; set; }
        public int? ScopeId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual Scope Scope { get; set; }
    }
}
