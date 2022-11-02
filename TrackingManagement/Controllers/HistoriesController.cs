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
    [Route("api/histories")]
    [ApiController]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoriesController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getHistory([FromQuery]HistoryFilter historyFilter)
        {
            HistoryAndKmCar queryRs = await _historyService.getHistory(historyFilter);
            BaseResponse<HistoryAndKmCar> res = new BaseResponse<HistoryAndKmCar>(queryRs);
            return Ok(res);
        }
    }
}
