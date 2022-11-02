using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Services
{
    public class OnlineService : IOnlineService
    {
        private readonly BKContext _db;

        public OnlineService(BKContext db)
        {
            _db = db;
        }

        public List<Online> getCarsStatus()
        {
            List<Online> carsStatus = _db.Onlines.Include(item => item.Car).ToList();
            return carsStatus;
        }
    }
}
