using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IRouteService
    {
        public Task<PagedResponse<List<Route>>> getRoutes(PaginationFilter paginationFilter, RouteFilter routeFilter, List<int> unitIds);
        public int createRoute(SampleRoute sampleRoute);
        public int updateRoute(UpdateRoute updateRoute, int routeId);

    }
}
