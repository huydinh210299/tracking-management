using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;

namespace TrackingManagement.Services
{
    public class SegmentationService : ISegmentationService
    {
        private BKContext _db;

        public SegmentationService(BKContext db)
        {
            _db = db;
        }

        public int createSegmentation(Segmentation segmentation)
        {
            _db.Segmentations.Add(segmentation);
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<Segmentation>>> getSegmentations(PaginationFilter paginationFilter, SegmentationFilter segmentationFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.Segmentations
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(segmentationFilter)
                                    .Include(item => item.Route)
                                    .Include(item => item.EditedSegmentationRoutes);
            var total = await _db.Segmentations
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(segmentationFilter).CountAsync();
            List<Segmentation> queryRs = new List<Segmentation>();
            if (paginationFilter.Paging == true)
            {
                queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
            }
            else
            {
                queryRs = await query.ToListAsync();
            }
            return new PagedResponse<List<Segmentation>>(queryRs, total);
        }

        public int updateSegmentation(UpdateSegmentation updateSegmentation, int Id)
        {
            var baseRecord = _db.Segmentations.Where(item => item.Id == Id).FirstOrDefault();
            var updateRecord = EntityUtils.updateRecord(baseRecord, updateSegmentation);
            return _db.SaveChanges();
        }

        public int updateSegmentationRoute(UpdateSegmentationRoute updateSegmentationRoute)
        {
            EditedSegmentationRoute segmentationRoute = _db.EditedSegmentationRoutes
                                                        .Where(item => item.SegmentationId == updateSegmentationRoute.SegmentationId
                                                        && DateTime.Compare(item.EditedIn, updateSegmentationRoute.EditedIn) == 0).FirstOrDefault();
            if (segmentationRoute != null)
            {
                EntityUtils.updateRecord(segmentationRoute, updateSegmentationRoute);
            }
            else
            {
                EditedSegmentationRoute newRecord = new EditedSegmentationRoute() { 
                    Route = updateSegmentationRoute.Route,
                    Direction = updateSegmentationRoute.Direction,
                    WayBack = updateSegmentationRoute.WayBack,
                    AutoTurnBack = updateSegmentationRoute.AutoTurnBack,
                    SegmentationId = updateSegmentationRoute.SegmentationId,
                    EditedIn = updateSegmentationRoute.EditedIn
                };
                _db.EditedSegmentationRoutes.Add(newRecord);
            }
            return _db.SaveChanges();
        }
    }
}
