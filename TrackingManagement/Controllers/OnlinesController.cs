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
    public class OnlinesController : ControllerBase
    {
        private IOnlineService _onlineService;

        public OnlinesController(IOnlineService onlineService)
        {
            _onlineService = onlineService;
        }
        [HttpGet]
        [Route("")]
        public IActionResult getCarStatus()
        {
            List<Online> carStatus = _onlineService.getCarsStatus();
            BaseResponse<List<Online>> res = new BaseResponse<List<Online>>() { Data = carStatus };
            return Ok(res);
        }
    }
}
