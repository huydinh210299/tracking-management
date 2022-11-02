using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingManagement.DTO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IConfiguration _config;
        private IPermissionService _permissionService;
        private BKContext _db;
        public PermissionsController(ILogger<PermissionsController> logger, IConfiguration config, IPermissionService permissionService, BKContext db)
        {
            _logger = logger;
            _config = config;
            _permissionService = permissionService;
            _db = db;
        }

        [HttpGet]
        [Route("scopes")]
        public IActionResult getScopePermission([FromQuery] PermissionQuery query)
        {
            var permisisons = _permissionService.getScopePermission(query);
            return Ok(permisisons);
        }

        [HttpPost]
        [Route("scopes")]
        public  IActionResult createScopePermission([FromBody] ScopePermissionCreated scopePermissionCreated)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {
                var scope = _permissionService.createScopePermission(scopePermissionCreated);
                transaction.Commit();
                return Ok(new Response<Scope>() { Data = scope, StatusCode = 200});
            }
            catch(Exception e)
            {
                transaction.Rollback();
                return Ok(new Response<string>() { Data = "Xảy ra lỗi trong quá trình lưu", StatusCode = 500 });
            }
        }

        [HttpGet]
        [Route("")]
        public IActionResult getAllPermission()
        {
            var permisisons = _permissionService.getAllPermission();
            return Ok(permisisons);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> updatePermission([FromBody] UpdatedScopePermission updatedScopePermission)
        {
            var result = await _permissionService.updatePermission(updatedScopePermission);
            Response<string> res = new Response<string>();
            if (result >= 0)
            {
                res.StatusCode = 200;
                res.Data = "Cập nhật thành công";
            }
            else
            {
                res.StatusCode = 500;
                res.Data = "Trong quá trình cập nhật xảy ra lỗi";
            }
            return Ok(res);
        }

        [HttpGet]
        [Route("allowed-routes/{scopeId}")]
        public IActionResult getScopeAllowRoute(int scopeId)
        {
            List<string> allowRouteList = _permissionService.getScopeAllowedRoute(scopeId);
            BaseResponse<List<string>> res = new BaseResponse<List<string>>(allowRouteList);
            return Ok(res);
        }

        [HttpPut]
        [Route("allowed-routes")]
        public IActionResult updateScopeAllowedRoute([FromBody] UpdateScopeAllowedRoute updateScopeAllowedRoute)
        {
            int updateResult = _permissionService.updateScopeAllowedRoute(updateScopeAllowedRoute);
            BaseResponse<string> res = new BaseResponse<string>();
            if(updateResult >= 0)
            {
                res.Succeeded = true;
                res.Message = "Cập nhật thành công";
            }
            else
            {
                res.Succeeded = false;
                res.Message = "Cập nhật thất bại";
            }
            return Ok(res);
        }
    }
}
