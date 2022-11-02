using System.Collections.Generic;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IOnlineService
    {
        public List<Online> getCarsStatus();
    }
}
