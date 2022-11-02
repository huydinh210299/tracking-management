using AutoFilter;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
    public class RouteService : IRouteService
    {
        private BKContext _db;

        public RouteService(BKContext db)
        {
            _db = db;
        }

        public int createRoute(SampleRoute sampleRoute)
        {
            _db.SampleRoutes.Add(sampleRoute);
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<Route>>> getRoutes(PaginationFilter paginationFilter, RouteFilter routeFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var query = _db.SampleRoutes
                                    .Where(item => unitIds.Contains((int)item.UnitId))
                                    .AutoFilter(routeFilter)
                                    .Include(item => item.Unit);
            var total = await _db.SampleRoutes
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(routeFilter).CountAsync();
            List<SampleRoute> queryRs = new List<SampleRoute>();
            if (paginationFilter.Paging == true)
            {
                queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
            }
            else
            {
                queryRs = await query.ToListAsync();
            }
            List<Route> routes = new List<Route>();
            foreach(SampleRoute item in queryRs)
            {
                var waypoints = JsonConvert.DeserializeObject<List<RouteItem>>(item.Route);
                var route = new Route() { RouteInfo = item, WayPoints = waypoints };
                routes.Add(route);
            }
            return new PagedResponse<List<Route>>(routes, total);
        }

        public int updateRoute(UpdateRoute updateRoute, int routeId)
        {
            var baseRecord = _db.SampleRoutes.Where(item => item.Id == routeId).FirstOrDefault();
            var updateRecord = EntityUtils.updateRecord(baseRecord, updateRoute);
            return _db.SaveChanges();
        }
    }
}
