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
    public class TransactionPointService : ITransactionPointService
    {
        private BKContext _db;

        public TransactionPointService(BKContext db)
        {
            _db = db;
        }

        public TransactionPoint createTransactionPoint(TransactionPoint transactionPoint)
        {
            _db.TransactionPoints.Add(transactionPoint);
            _db.SaveChanges();
            return transactionPoint;
        }

        public async Task<PagedResponse<List<TransactionPoint>>> getTransactionPoint(PaginationFilter paginationFilter, TransactionPointFilter transactionPointFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.TransactionPoints
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(transactionPointFilter)
                                    .Include(item => item.Unit);
            var total = await _db.TransactionPoints
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(transactionPointFilter).CountAsync();
            if (paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<TransactionPoint>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<TransactionPoint>>(queryRs, total);
            }
        }

        public TransactionPoint updateTransactionPoint(UpdateTransactionPoint updateTransactionPoint, int id)
        {
            var baseRecord = _db.TransactionPoints.Where(item => item.Id == id).FirstOrDefault();
            var updateRecord = EntityUtils.updateRecord(baseRecord, updateTransactionPoint);
            _db.SaveChanges();
            return updateRecord;
        }
    }
}
