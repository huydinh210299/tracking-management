using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Services
{
    public class PermissionService : IPermissionService
    {
        private BKContext _db;

        public PermissionService(BKContext db)
        {
            _db = db;
        }

        public List<Scope> getAllScope()
        {
            var scopes = (from sc in _db.Scopes select sc).ToList();
            return scopes;
        }

        public List<ScopePermission> getScopePermission(PermissionQuery query)
        {
            var scopeId = query.scopeId;
            var permissions = _db.ScopePermissions.Include("Permission").Where(item => item.ScopeId == scopeId).ToList();
            return permissions;
        }

        public async Task<int> updatePermission(UpdatedScopePermission updatedScopePermission)
        {
            var changePermissions = JsonConvert.DeserializeObject<List<ChangedPermission>>(updatedScopePermission.PermissionChange);

            if(changePermissions.Count > 0)
            {
                foreach (ChangedPermission item in changePermissions)
                {
                    var scopePermission = await (from scp in _db.ScopePermissions
                                           where scp.Id == item.Id
                                           select scp).FirstOrDefaultAsync();
                    scopePermission.Allowed = item.Allowed;
                    scopePermission.Filter = item.Filter != null ? JsonConvert.SerializeObject(item.Filter) : "";
                }
            }

            var deactivePermission = JsonConvert.DeserializeObject<List<int>>(updatedScopePermission.PermissionDeActive);
            if(deactivePermission.Count > 0)
            {
                foreach(int Id  in deactivePermission)
                {
                    
                    var scopePermission = await (from scp in _db.ScopePermissions
                                                    where scp.Id == Id
                                                    select scp).FirstOrDefaultAsync();
                    scopePermission.Allowed = "false";
                    scopePermission.Filter = "";
                }
            }
            return await _db.SaveChangesAsync();
        }

        public List<Permission> getAllPermission()
        {
            var permissions = (from p in _db.Permissions select p).ToList();
            return permissions;
        }

        public Scope createScopePermission(ScopePermissionCreated scopePermissionCreated)
        {
            var scopeName = scopePermissionCreated.scopeName;
            Scope scope = new Scope() { Name = scopeName, AllowedRoute = scopePermissionCreated.allowedRoute };
            _db.Scopes.Add(scope);
            _db.SaveChanges();


            var allPermissions = (from p in _db.Permissions select p).ToList();
            var listScopePermissionData = JsonConvert.DeserializeObject<List<PermissionData>>(scopePermissionCreated.permissionData);
            var allowedPermissionIds = new List<int>();
            foreach (PermissionData item in listScopePermissionData)
            {
                allowedPermissionIds.Add(item.PermissionId);
            }
            var notAllowedPermissions = allPermissions.Where(p => !allowedPermissionIds.Contains(p.Id)).ToList();

            foreach (PermissionData item in listScopePermissionData)
            {
                _db.ScopePermissions.Add(new ScopePermission()
                {
                    Allowed = "true",
                    Filter = item.Filter.Count == 0 ? "" : JsonConvert.SerializeObject(item.Filter),
                    PermissionId = item.PermissionId,
                    ScopeId = scope.Id
                });
            }

            foreach (Permission item in notAllowedPermissions)
            {
                _db.ScopePermissions.Add(new ScopePermission()
                {
                    Allowed = "false",
                    Filter = "",
                    PermissionId = item.Id,
                    ScopeId = scope.Id
                });
            }

            _db.SaveChanges();

            return scope;
        }

        public List<string> getScopeAllowedRoute(int scopeId)
        {
            Scope scope = _db.Scopes.Where(item => item.Id == scopeId).FirstOrDefault();
            return JsonConvert.DeserializeObject<List<string>>(scope.AllowedRoute);
        }

        public int updateScopeAllowedRoute(UpdateScopeAllowedRoute updateScopeAllowedRoute)
        {
            Scope scope = _db.Scopes.Where(item => item.Id == updateScopeAllowedRoute.ScopeId).FirstOrDefault();
            if(scope != null)
            {
                scope.AllowedRoute = updateScopeAllowedRoute.AllowedRoutes;
                return _db.SaveChanges();
            }
            return -1;
        }
    }
}
