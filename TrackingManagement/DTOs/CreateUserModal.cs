using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingManagement.DTOs
{
    public class CreateUserModal
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? ScopeId { get; set; }
        public List<int> UnitIds { get; set; }
    }
}
