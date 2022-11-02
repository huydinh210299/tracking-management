using System;
using System.Collections.Generic;

#nullable disable

namespace TrackingManagement.Models
{
    public partial class Permission
    {
        public Permission()
        {
            ScopePermissions = new HashSet<ScopePermission>();
        }

        public int Id { get; set; }
        public string Resource { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Action { get; set; }
        public string Filter { get; set; }
        public string FilterValue { get; set; }

        public virtual ICollection<ScopePermission> ScopePermissions { get; set; }
    }
}
