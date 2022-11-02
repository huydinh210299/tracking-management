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
    [Route("api/rfids")]
    [ApiController]
    public class RFIDsController : ControllerBase
    {
        private readonly ILogger<RFIDsController> _logger;
        private readonly IConfiguration _config;
        private IRFIDService _rfidService;

        public RFIDsController(ILogger<RFIDsController> logger, IConfiguration config, IRFIDService rfidService)
        {
            _logger = logger;
            _config = config;
            _rfidService = rfidService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult createRFID(Rfid rfid)
        {
            return Ok(_rfidService.createRfid(rfid));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getRfid([FromQuery]RfidFilter rfidFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rfids = await _rfidService.getRfid(rfidFilter, paginationFilter, unitIds);
            return Ok(rfids);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult updateRfid([FromBody] RfidFilter rfid, int id)
        {
            var updatedRfid = _rfidService.updateRfid(rfid, id);
            return Ok(updatedRfid);
        }
    }
}
