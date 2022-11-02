using System.Collections.Generic;

namespace TrackingManagement.DTOs
{
    public class UpdateUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ScopeId { get; set; }
        public List<int> UnitIds { get; set; }
    }
}
