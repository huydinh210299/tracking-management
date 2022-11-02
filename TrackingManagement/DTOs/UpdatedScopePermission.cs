using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdatedScopePermission
    {
        public string PermissionChange { get; set; }
        public string PermissionDeActive { get; set; }
    }

    public class ChangedPermission
    {
        public int Id { get; set; }
        public string Allowed { get; set; }
        public List<RequestFilter> Filter { get; set; }
    }

    public class DeActivePermission
    {
        public List<int> Ids { get; set; }
    }
}

