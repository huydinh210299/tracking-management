using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class ScopePermissionCreated
    {
        public string scopeName { get; set; }
        public string permissionData { get; set; }
        public string allowedRoute { get; set; }
    }

    public class PermissionData
    {
        public int PermissionId { get; set; }
        public List<RequestFilter> Filter { get; set; }
    }
}
