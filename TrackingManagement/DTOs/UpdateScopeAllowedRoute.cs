using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class UpdateScopeAllowedRoute
    {
        public int ScopeId { get; set; }
        public string AllowedRoutes { get; set; }
    }
}
