using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IMemberService
    {
        public Task<PagedResponse<List<Driver>>> getDrivers(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds);
        public Task<PagedResponse<List<Atmtechnican>>> getAtmTechnicans(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds);
        public Task<PagedResponse<List<Treasurer>>> getTreasurers(MemberFilter memberFilter, PaginationFilter paginationFilter, List<int> unitIds);
        public int createDriver(Driver driver);
        public int createAtmTechnican(Atmtechnican atmtechnican);
        public int createTreasure(Treasurer treasurer);
        public int updateDriver(int driverId, UpdateMember updateMember);
        public int updateAtmTechnican(int atmtechnicanId, UpdateMember updateMember);
        public int updateTreasure(int treasureId, UpdateMember updateMember);
    }
}
