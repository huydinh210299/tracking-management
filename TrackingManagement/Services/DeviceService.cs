using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;

namespace TrackingManagement.Services
{
    public class DeviceService: IDeviceService
    {
        private BKContext _db;

        public DeviceService(BKContext db)
        {
            _db = db;
        }

        public int createDevice(Device device)
        {
            _db.Devices.Add(device);
            var rs = _db.SaveChanges();
            return rs;
        }

        public async Task<PagedResponse<List<Device>>> getDevice(PaginationFilter paginationFilter, DeviceFilter deviceFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var total = await _db.Devices
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(deviceFilter).CountAsync();
            var query = _db.Devices
                            .Where(item => unitIds.Contains((int)item.UnitId))
                            .AutoFilter(deviceFilter)
                            .Include(item => item.Car)
                            .Include(item => item.Unit);
            if (paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Device>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<Device>>(queryRs, total);
            }
        }

        public int updateDevice(UpdateDevice updateDevice, int deviceId)
        {
            var device = _db.Devices.Where(item => item.Id == deviceId).FirstOrDefault();
            var udpateRs = EntityUtils.updateRecord(device, updateDevice);
            return _db.SaveChanges();
        }
    }
}
