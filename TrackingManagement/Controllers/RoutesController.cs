using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class RoutesController : ControllerBase
    {
        private IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getRoute([FromQuery]PaginationFilter paginationFilter, [FromQuery] RouteFilter routeFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _routeService.getRoutes(paginationFilter, routeFilter, unitIds);
            return Ok(rs);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createRoute([FromBody] SampleRoute sampleRoute)
        {
            var rs = _routeService.createRoute(sampleRoute);
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
        [Route("{Id}")]
        public IActionResult updateRoute(int Id,[FromBody] UpdateRoute updateRoute)
        {
            var updateRs = _routeService.updateRoute(updateRoute, Id);
            var response = new BaseResponse<String>();
            if (updateRs > 0)
            {
                response.Message = "Cập nhật thành công";
            } else
            {
                response.Succeeded = false;
                response.Errors = "Cập nhật thất bại";
            }

            return Ok(response);
        }
    }
}
