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
    [Route("api/transaction-points")]
    [ApiController]
    public class TransactionPointsController : ControllerBase
    {
        private readonly ILogger<TransactionPointsController> _logger;
        private readonly IConfiguration _config;
        private ITransactionPointService _transactionPointService;

        public TransactionPointsController(ILogger<TransactionPointsController> logger, IConfiguration config, ITransactionPointService transactionPointService)
        {
            _logger = logger;
            _config = config;
            _transactionPointService = transactionPointService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getTransactionPoints([FromQuery] PaginationFilter paginationFilter, [FromQuery] TransactionPointFilter transactionPointFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var queryRs = await _transactionPointService.getTransactionPoint(paginationFilter, transactionPointFilter, unitIds);
            return Ok(queryRs);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createTransactionPoint([FromBody] TransactionPoint transactionPoint)
        {
            var createdtransactionPoint = _transactionPointService.createTransactionPoint(transactionPoint);
            var res = new BaseResponse<TransactionPoint>() { Data = createdtransactionPoint };
            return Ok(res);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult updateTransactionPoint(int id, [FromBody] UpdateTransactionPoint updateTransactionPoint)
        {
            var updatedTransactionPoint = _transactionPointService.updateTransactionPoint(updateTransactionPoint, id);
            var res = new BaseResponse<TransactionPoint>() { Data = updatedTransactionPoint };
            return Ok(res);
        }

    }
}
