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
    public class RFIDService : IRFIDService
    {
        private readonly BKContext _db;

        public RFIDService(BKContext db)
        {
            _db = db;
        }

        public Rfid createRfid(Rfid rfid)
        {
            _db.Rfids.Add(rfid);
            _db.SaveChanges();
            return rfid;
        }

        public async Task<PagedResponse<List<Rfid>>> getRfid(RfidFilter rfidFilter, PaginationFilter paginationFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Rfids.AutoFilter(rfidFilter).
                                        Where(item => unitIds.Contains((int)item.UnitId))
                                        .Include(item => item.Unit);
            var total = await _db.Rfids.AutoFilter(rfidFilter).Where(item => unitIds.Contains((int)item.UnitId)).CountAsync();
            if (paginationFilter.Paging == true)
            {
                var rfids = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Rfid>>(rfids, total);
            }
            else
            {
                var rfids = await query.ToListAsync();
                return new PagedResponse<List<Rfid>>(rfids, total);
            }
        }

        public Rfid updateRfid(RfidFilter rfid, int id)
        {
            var baseRfid = _db.Rfids.FirstOrDefault(item => item.Id == id);
            Rfid updatedRfid = EntityUtils.updateRecord(baseRfid, rfid);
            _db.SaveChanges();
            return updatedRfid;
        }
    }
}
