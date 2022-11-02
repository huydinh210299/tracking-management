using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingManagement.DTO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _config;
        private IUserService _userService;
        private JwtHandler tokenHandler = new JwtHandler();

        public UsersController(ILogger<UsersController> logger, IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _config = configuration;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login([FromBody] UserLoginModel userInfo)
        {
            var userScope = _userService.getUserScope(userInfo);
            if(userScope != null)
            {
                var user = userScope.Item1;
                var scope = userScope.Item2;
                var secretKey = _config.GetSection("JwtSetting:SecretKey").Get<string>();
                var token = tokenHandler.GenerateJwtToken(user.Id,scope.Id.ToString(), secretKey);
                var response = new Response<AccessTokenModel>();
                response.StatusCode = 200;
                response.Data = new AccessTokenModel(token, scope.Id);
                return Ok(response);
            }
            else
            {
                var response = new Response<string>();
                response.StatusCode = 401;
                response.Data = "Unauthorize";
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("units")]
        public  IActionResult getUserUnit()
        {
            int userId = (int)HttpContext.Items["UID"];
            var userUnits =  _userService.getUserUnit(userId);
            return Ok(new BaseResponse<List<Unit>>(){ Data = userUnits});
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> getListUser([FromQuery] UserFilter userFilter,[FromQuery] PaginationFilter paginationFilter)
        {
            var response = await _userService.getListUser(userFilter, paginationFilter);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult getUserById(int id)
        {
            var user = _userService.getUserById(id);
            var response = new BaseResponse<User>() { Data = user };
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IActionResult createUser([FromBody] CreateUserModal createUserModal)
        {
            var rs = _userService.createUser(createUserModal);
            BaseResponse<string> res = new BaseResponse<string>();
            if (rs == 1)
            {
                res.Message = "Tạo tài khoản thành công";
                res.Succeeded = true;
            }
            else
            {
                res.Message = "Tạo tài khoản thất bại";
                res.Succeeded = false;
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("{Id}")]
        public IActionResult updateUser([FromBody] UpdateUser updateUser,int Id)
        {
            int queryRs = _userService.updateUser(updateUser, Id);
            BaseResponse<string> res = new BaseResponse<string>();
            if(queryRs != 0)
            {
                res.Message = "Cập nhật thành công";
            }
            else
            {
                res.Message = "Cập nhật thất bại";
                res.Succeeded = false;
            }
            return Ok(res);
        }

        [HttpDelete]
        [Route("{Id}")]
        public IActionResult deleteUser(int Id)
        {
            int deleteRs = _userService.deleteUser(Id);
            BaseResponse<string> res = new BaseResponse<string>();
            if(deleteRs == 0)
            {
                res.Succeeded = false;
                res.Message = "Xoá không thành công";
            }
            else
            {
                res.Message = "Xoá tài khoản thành công";
            }
            return Ok(res);
        }

        [HttpPut]
        [Route("password")]
        public IActionResult updatePassword(UpdateUserPassword updateUserPassword)
        {
            int updateRs = _userService.updateUserPassword(updateUserPassword);
            BaseResponse<string> res = new BaseResponse<string>();
            if(updateRs == 0)
            {
                res.Succeeded = false;
                res.Message = "Cập nhật thất bại. Mời bạn thử lại!";
            }
            else
            {
                res.Succeeded = true;
                res.Message = "Cập nhật thành công!";
            }
            return Ok(res);
        }

    }
}
