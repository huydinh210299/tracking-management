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
    public class MemberService : IMemberService
    {
        private readonly BKContext _db;

        public MemberService(BKContext db)
        {
            _db = db;
        }

        public int createAtmTechnican(Atmtechnican atmtechnican)
        {
            _db.Atmtechnicans.Add(atmtechnican);
            var rfid = _db.Rfids.Where(item => item.Id == atmtechnican.Rfidid).FirstOrDefault();
            if(rfid != null)
            {
                rfid.IsDistributed = true;
            }
            return _db.SaveChanges();
        }

        public int createDriver(Driver driver)
        {
            _db.Drivers.Add(driver);
            var rfid = _db.Rfids.Where(item => item.Id == driver.Rfidid).FirstOrDefault();
            if (rfid != null)
            {
                rfid.IsDistributed = true;
            }
            return _db.SaveChanges();
        }

        public int createTreasure(Treasurer treasurer)
        {
            _db.Treasurers.Add(treasurer);
            var rfid = _db.Rfids.Where(item => item.Id == treasurer.Rfidid).FirstOrDefault();
            if (rfid != null)
            {
                rfid.IsDistributed = true;
            }
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<Atmtechnican>>> getAtmTechnicans(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Atmtechnicans
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(memberFilter)
                                    .Include(item => item.Rfid)
                                    .Include(item => item.Unit);
            var total = await _db.Atmtechnicans
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(memberFilter).CountAsync();
            if (paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Atmtechnican>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<Atmtechnican>>(queryRs, total);
            }
        }

        public async Task<PagedResponse<List<Driver>>> getDrivers(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var total = await _db.Drivers
                        .AutoFilter(memberFilter)
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .CountAsync();
            var query = _db.Drivers.AutoFilter(memberFilter)
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .Include(item => item.Rfid)
                                    .Include(item => item.Unit);
            if(paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Driver>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<Driver>>(queryRs, total);
            }
        }

        public async Task<PagedResponse<List<Treasurer>>> getTreasurers(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var total = await _db.Treasurers
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(memberFilter).CountAsync();
            var query = _db.Treasurers
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(memberFilter)
                                    .Include(item => item.Rfid)
                                    .Include(item => item.Unit);
            if(paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Treasurer>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<Treasurer>>(queryRs, total);
            }

        }

        public int updateAtmTechnican(int atmtechnicanId, UpdateMember updateMember)
        {
            var atmTechnican = _db.Atmtechnicans.Where(item => item.Id == atmtechnicanId).FirstOrDefault();
            if (updateMember.Rfidid != null)
            {
                var newRfid = _db.Rfids.Where(item => item.Id == updateMember.Rfidid).FirstOrDefault();
                if (atmTechnican.Rfidid != null)
                {
                    if (atmTechnican.Rfidid != updateMember.Rfidid)
                    {

                        var currentRfid = _db.Rfids.Where(item => item.Id == atmTechnican.Rfidid).FirstOrDefault();
                        currentRfid.IsDistributed = false;
                    }
                }
                newRfid.IsDistributed = true;
            }
            var udpateRs = EntityUtils.updateRecord(atmTechnican, updateMember);
            return _db.SaveChanges();
        }

        public int updateDriver(int driverId, UpdateMember updateMember)
        {
            var driver = _db.Drivers.Where(item => item.Id == driverId).FirstOrDefault();
            if (updateMember.Rfidid != null)
            {
                var newRfid = _db.Rfids.Where(item => item.Id == updateMember.Rfidid).FirstOrDefault();
                if (driver.Rfidid != null)
                {
                    if (driver.Rfidid != updateMember.Rfidid)
                    {

                        var currentRfid = _db.Rfids.Where(item => item.Id == driver.Rfidid).FirstOrDefault();
                        currentRfid.IsDistributed = false;
                    }
                }
                newRfid.IsDistributed = true;
            }
            var udpateRs = EntityUtils.updateRecord(driver, updateMember);
            return _db.SaveChanges();
        }

        public int updateTreasure(int treasureId, UpdateMember updateMember)
        {   
            var treasurer = _db.Treasurers.Where(item => item.Id == treasureId).FirstOrDefault();
            if (updateMember.Rfidid != null)
            {
                var newRfid = _db.Rfids.Where(item => item.Id == updateMember.Rfidid).FirstOrDefault();
                if (treasurer.Rfidid != null)
                {
                    if (treasurer.Rfidid != updateMember.Rfidid)
                    {

                        var currentRfid = _db.Rfids.Where(item => item.Id == treasurer.Rfidid).FirstOrDefault();
                        currentRfid.IsDistributed = false;
                    }
                }
                newRfid.IsDistributed = true;
            }
            var udpateRs = EntityUtils.updateRecord(treasurer, updateMember);
            return _db.SaveChanges();
        }
    }
}
