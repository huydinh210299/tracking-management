using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IUnitService
    {
        public Task<PagedResponse<List<Unit>>> getListUnit(UnitFilter unitFilter, PaginationFilter paginationFilter);
        public int createUnit(Unit unit);
        public int updateUnit(UpdateUnit updateUnit, int unitId);
    }
}
