using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IHistoryService
    {
        public Task<HistoryAndKmCar> getHistory(HistoryFilter historyFilter);
    }
}
