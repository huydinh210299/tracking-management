using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IRFIDService
    {
        public Rfid createRfid(Rfid rfid);
        public Task<PagedResponse<List<Rfid>>> getRfid(RfidFilter rfidFilter, PaginationFilter paginationFilter, List<int> unitIds);
        public Rfid updateRfid(RfidFilter rfid, int id);
    }
}
