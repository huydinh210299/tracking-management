using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IPermissionService
    {
        public List<Scope> getAllScope();
        public List<ScopePermission> getScopePermission(PermissionQuery query);
        public Scope createScopePermission(ScopePermissionCreated scopePermissionCreated);
        public Task<int> updatePermission(UpdatedScopePermission updatedScopePermission);
        public List<Permission> getAllPermission();
        public List<string> getScopeAllowedRoute(int scopeId);
        public int updateScopeAllowedRoute(UpdateScopeAllowedRoute updateScopeAllowedRoute);
    }
}
