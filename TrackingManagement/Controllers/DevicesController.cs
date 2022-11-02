using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly ILogger<DevicesController> _logger;
        private readonly IConfiguration _config;
        private IDeviceService _deviceService;

        public DevicesController(ILogger<DevicesController> logger, IConfiguration config, IDeviceService deviceService)
        {
            _logger = logger;
            _config = config;
            _deviceService = deviceService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getDevices([FromQuery]PaginationFilter paginationFilter, [FromQuery] DeviceFilter deviceFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _deviceService.getDevice(paginationFilter, deviceFilter, unitIds);
            return Ok(rs);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createDevice([FromBody]Device device)
        {
            var rs = _deviceService.createDevice(device);
            BaseResponse<string> res = new BaseResponse<string>();
            if (rs == 1)
            {
                res.Message = "Tạo mới thành công";
            }
            else
            {
                res.Succeeded = false;
                res.Errors = "Trong quá trình tạo mới xảy ra lỗi";
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult updateDevice([FromBody] UpdateDevice device, int id)
        {
            var rs = _deviceService.updateDevice(device, id);
            BaseResponse<string> res = new BaseResponse<string>();
            if (rs == 1)
            {
                res.Message = "Cập nhật thành công";
            }
            else
            {
                res.Succeeded = false;
                res.Errors = "Cập nhật thất bại";
            }
            return Ok(res);
        }
    }
}
