using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IDeviceService
    {
        public Task<PagedResponse<List<Device>>> getDevice(PaginationFilter paginationFilter, DeviceFilter deviceFilter, List<int> unitIds);
        public int createDevice(Device device);
        public int updateDevice(UpdateDevice updateDevice, int deviceId);
    }
}
