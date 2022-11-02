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
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly IConfiguration _config;
        private ICarService _carService;

        public CarsController(ILogger<CarsController> logger, IConfiguration config, ICarService carService)
        {
            _logger = logger;
            _config = config;
            _carService = carService;
        }

        [HttpPost]
        [Route("")]
        public IActionResult createCar(Car car)
        {
            var rs = _carService.createCar(car);
            BaseResponse<string> res = new BaseResponse<string>();
            if(rs == 1)
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

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getCars([FromQuery] PaginationFilter paginationFilter, [FromQuery] CarFilter carFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var queryRs = await _carService.getCars(paginationFilter, carFilter, unitIds);
            return Ok(queryRs);
        }

        [HttpPut]
        [Route("{carId}")]
        public IActionResult updateCar(int carId, [FromBody] UpdateCar updateCar)
        {
            var updateRs = _carService.updateCar(updateCar, carId);
            var response = new BaseResponse<String>();
            if (updateRs == 1)
            {
                response.Message = "Cập nhật thành công";
            }
            else
            {
                response.Succeeded = false;
                response.Errors = "Cập nhật thất bại";
            }

            return Ok(response);
        }

    }
}
