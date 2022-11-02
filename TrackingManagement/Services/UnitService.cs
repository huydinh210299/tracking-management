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
    public class UnitService : IUnitService
    {
        private readonly BKContext _db;

        public UnitService(BKContext db)
        {
            _db = db;
        }

        public int createUnit(Unit unit)
        {
            _db.Units.Add(unit);
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<Unit>>> getListUnit(UnitFilter unitFilter, PaginationFilter paginationFilter)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var total = await _db.Units
                        .AutoFilter(unitFilter).CountAsync();
            var query = _db.Units
                            .AutoFilter(unitFilter);
            List<Unit> unitList;
            if (paginationFilter.Paging == true)
            {
                unitList = await query.Skip((page - 1) * record).Take(record).ToListAsync();
            }
            else
            {
                unitList = await query.ToListAsync();
            }
            return new PagedResponse<List<Unit>>(unitList, total);
        }

        public int updateUnit(UpdateUnit updateUnit, int unitId)
        {
            Unit baseRecord = _db.Units.Where(item => item.Id == unitId).FirstOrDefault();
            var updateRecord = EntityUtils.updateRecord(baseRecord, updateUnit);
            return _db.SaveChanges();
        }
    }
}
