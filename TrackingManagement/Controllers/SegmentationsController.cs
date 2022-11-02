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
    public class SegmentationsController : ControllerBase
    {
        private ISegmentationService _segmentationService;

        public SegmentationsController(ISegmentationService segmentationService)
        {
            _segmentationService = segmentationService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getSegmentation([FromQuery] PaginationFilter paginationFilter, [FromQuery] SegmentationFilter segmentationFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _segmentationService.getSegmentations(paginationFilter, segmentationFilter, unitIds);
            return Ok(rs);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createSegmentation([FromBody] Segmentation segmentation)
        {
            var rs = _segmentationService.createSegmentation(segmentation);
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
        public IActionResult updateSegmentation(int Id, [FromBody] UpdateSegmentation updateSegmentation)
        {
            var updateRs = _segmentationService.updateSegmentation(updateSegmentation, Id);
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

        [HttpPut]
        [Route("route")]
        public IActionResult updateSegmentationRoute([FromBody] UpdateSegmentationRoute updateSegmentationRoute)
        {
            var updateRs = _segmentationService.updateSegmentationRoute(updateSegmentationRoute);
            var response = new BaseResponse<String>();
            if (updateRs >= 0)
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
