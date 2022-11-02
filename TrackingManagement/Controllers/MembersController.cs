using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ILogger<MembersController> _logger;
        private readonly IConfiguration _config;
        private IMemberService _memberService;

        public MembersController(ILogger<MembersController> logger, IConfiguration config, IMemberService memberService)
        {
            _logger = logger;
            _config = config;
            _memberService = memberService;
        }

        [HttpGet]
        [Route("drivers")]
        public async Task<IActionResult> getDriver([FromQuery] MemberFilter memberFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _memberService.getDrivers(memberFilter, paginationFilter, unitIds);
            return Ok(rs);
        }

        [HttpGet]
        [Route("atm-technicans")]
        public async Task<IActionResult> getATMTechnican([FromQuery] MemberFilter memberFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _memberService.getAtmTechnicans(memberFilter, paginationFilter, unitIds);
            return Ok(rs);
        }

        [HttpGet]
        [Route("treasurers")]
        public async Task<IActionResult> getTreasurer([FromQuery] MemberFilter memberFilter, [FromQuery] PaginationFilter paginationFilter)
        {
            List<int> unitIds = (List<int>)HttpContext.Items["unitIds"];
            var rs = await _memberService.getTreasurers(memberFilter, paginationFilter, unitIds);
            return Ok(rs);
        }

        [HttpPost]
        [Route("drivers")]
        public IActionResult createDriver([FromBody] Driver driver)
        {
            var createdResult = _memberService.createDriver(driver);
            var rs = new BaseResponse<string>();
            if (createdResult == 1)
            {
                rs.Message = "Tạo mới thành công";
            }
            else
            {
                rs.Errors = "Tạo mới thất bại";
                rs.Succeeded = false;
            }
            return Ok(rs);
        }

        [HttpPost]
        [Route("atm-technicans")]
        public IActionResult createATMTechnican([FromBody] Atmtechnican atmtechnican)
        {
            var createdResult = _memberService.createAtmTechnican(atmtechnican);
            var rs = new BaseResponse<string>();
            if (createdResult == 1)
            {
                rs.Message = "Tạo mới thành công";
            }
            else
            {
                rs.Errors = "Tạo mới thất bại";
                rs.Succeeded = false;
            }
            return Ok(rs);
        }

        [HttpPost]
        [Route("treasurers")]
        public IActionResult createTreasurer([FromBody] Treasurer treasurer)
        {
            var createdResult = _memberService.createTreasure(treasurer);
            var rs = new BaseResponse<string>();
            if (createdResult == 1)
            {
                rs.Message = "Tạo mới thành công";
            }
            else
            {
                rs.Errors = "Tạo mới thất bại";
                rs.Succeeded = false;
            }
            return Ok(rs);
        }

        [HttpPut]
        [Route("drivers/{id}")]
        public IActionResult updateDriver(int id, [FromBody] UpdateMember updateMember)
        {
            var updateRs = _memberService.updateDriver(id, updateMember);
            var response = new BaseResponse<String>();
            if(updateRs == 1)
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
        [Route("atm-technicans/{id}")]
        public IActionResult updateATMTechnican(int id, [FromBody] UpdateMember updateMember)
        {
            var updateRs = _memberService.updateAtmTechnican(id, updateMember);
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
        [Route("treasurers/{id}")]
        public IActionResult updateTreasurer(int id, [FromBody] UpdateMember updateMember)
        {
            var updateRs = _memberService.updateTreasure(id, updateMember);
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

        [HttpPost]
        [Route("upload-avata")]
        public IActionResult uploadAvata([FromBody] ImageDetail imageDetail)
        {
            byte[] bytes = Convert.FromBase64String(imageDetail.Image.Substring(imageDetail.Image.LastIndexOf(',') + 1));
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save("wwwroot/avata/" + imageDetail.ImgName);
            return Ok();
        }
    }
}
