using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private IUnitService _unitService;

        public UnitsController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getUnits([FromQuery] UnitFilter unitFilter,[FromQuery] PaginationFilter paginationFilter)
        {
            var queryRs =  await _unitService.getListUnit(unitFilter, paginationFilter);
            return Ok(queryRs);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createUnit([FromBody] Unit unit)
        {
            int createdRs = _unitService.createUnit(unit);
            BaseResponse<string> res = new BaseResponse<string>();
            if(createdRs >= 0)
            {
                res.Message = "Tạo đơn vị mới thành công";
            }
            else
            {
                res.Succeeded = false;
                res.Message = "Tạo đơn vị mới thất bại";
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult updateUnit([FromBody]UpdateUnit updateUnit, int Id)
        {
            int updateRs = _unitService.updateUnit(updateUnit, Id);
            BaseResponse<string> res = new BaseResponse<string>();
            if (updateRs >= 0)
            {
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
