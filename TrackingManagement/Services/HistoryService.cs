using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Handle;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly BKContext _db;

        public HistoryService(BKContext db)
        {
            _db = db;
        }

        public async Task<HistoryAndKmCar> getHistory(HistoryFilter historyFilter)
        {
            List<History> histories =  await _db.Histories.Where(item => item.CarId == historyFilter.CarId).AutoFilter(historyFilter).OrderBy(item => item.DeviceTime).ToListAsync();
            KmCar statistical = HistoryHandler.GetKmCarStatistical(histories);
            HistoryAndKmCar rs = new HistoryAndKmCar() { Histories = histories, Statistical = statistical };
            return rs;
        }
    }
}
