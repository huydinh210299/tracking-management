using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly ILogger<PermissionsController> _logger;
        private readonly IConfiguration _config;
        private IPermissionService _permissionService;

        public ScopesController(ILogger<PermissionsController> logger, IConfiguration config, IPermissionService permissionService)
        {
            _logger = logger;
            _config = config;
            _permissionService = permissionService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult getAllScope()
        {
            var scopes = _permissionService.getAllScope();
            return Ok(scopes);
        }
    }
}
