using TrackingManagement.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TrackingManagement.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TrackingManagement.Constants;
using System.IO;
using System.Text;
using TrackingManagement.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingManagement.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        //private BKContext _db = new BKContext();
        private JwtHandler jwtHandler = new JwtHandler();
        private readonly IConfiguration _config;

        public AuthorizeMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            BKContext _db = httpContext.RequestServices.GetService<BKContext>();
            var secretKey = _config.GetSection("JwtSetting:SecretKey").Get<string>();
            var allowPath = _config.GetSection("AllowPath").Get<List<string>>();

            // get route Url
            //var endPoint = httpContext.GetEndpoint();
            var route = (httpContext.Features.Get<IEndpointFeature>()?.Endpoint as RouteEndpoint)?.RoutePattern.RawText;
            route = "/" + route.ToLower();

            //check if current path in allowPath -> dont have to authorization
            var allowCurrentPath = allowPath.FirstOrDefault(value => value == route);
            if (allowCurrentPath == null)
            {
                var token = String.IsNullOrEmpty(httpContext.Request.Headers["x-access-token"]) ? "" : (string)httpContext.Request.Headers["x-access-token"];
                var decodeToken = jwtHandler.ValidateJwtToken(token, secretKey);
                if (decodeToken != null)
                {
                    // Get method : GET, POST, ... 
                    var method = httpContext.Request.Method;

                    // Get scopeId, userId
                    var userId = decodeToken.Item1;
                    var scopeId = decodeToken.Item2;

                    var userUnits = _db.UserUnits.Where(item => item.UserId == userId).Select(item => (int)item.UnitId).ToList();
                    //var userUnits = userService.getUserUnit(userId);

                    // Thêm thông tin userId và scopeId vào httpContext
                    httpContext.Items["UID"] = userId;
                    httpContext.Items["scopeId"] = scopeId;
                    httpContext.Items["unitIds"] = userUnits;

                    // Get permission
                    var permission = (from pm in _db.Permissions
                                      where pm.Url == route && pm.Method == method
                                      select pm).FirstOrDefault();
                    if (permission != null)
                    {
                        // Get scope permission
                        var scopePermission = (from spm in _db.ScopePermissions
                                               where spm.ScopeId == scopeId && spm.PermissionId == permission.Id
                                               select spm).FirstOrDefault();
                        // Nếu quyền được truy cập
                        if(scopePermission.Allowed == "true")
                        {
                            if(permission.Filter != "")
                            {
                                var filter = scopePermission.Filter;
                                var filterArr = JArray.Parse(filter);
                                IList<RequestFilter> filterResult = new List<RequestFilter>();
                                foreach (JObject item in filterArr)
                                {
                                    RequestFilter requestFilter = item.ToObject<RequestFilter>();
                                    filterResult.Add(requestFilter);
                                }

                                var filterType = permission.Filter;
                                switch (filterType)
                                {
                                    case FilterType.Body:
                                        //Read body
                                        httpContext.Request.EnableBuffering();

                                        // Leave the body open so the next middleware can read it.
                                        using (var reader = new StreamReader(
                                            httpContext.Request.Body,
                                            encoding: Encoding.UTF8,
                                            detectEncodingFromByteOrderMarks: false,
                                            bufferSize: 1024 * 45,
                                            leaveOpen: true))
                                        {
                                            var body = await reader.ReadToEndAsync();
                                            var bodyObj = JObject.Parse(body);

                                            foreach (RequestFilter item in filterResult)
                                            {
                                                var filterField = item.Field;
                                                var filterValues = item.Value;
                                                var bodyFilterValue = bodyObj[filterField];
                                                if (bodyFilterValue != null)
                                                {
                                                    var allowed = filterValues.FirstOrDefault(x => x.ValueName == bodyFilterValue.ToString());
                                                    //Nếu tham số lọc không nằm trong các giá trị cho phép thì trả về unAuthorize
                                                    if (allowed == null)
                                                    {
                                                        await ReturnUnAuthorizeResponse(httpContext);
                                                    }
                                                }
                                            }
                                            // Đưa stream body về vị trí 0 để các middleware tiếp theo có thể đọc
                                            httpContext.Request.Body.Position = 0;
                                            // Tiếp tục middleware tiếp theo
                                        }
                                        await _next.Invoke(httpContext);
                                        break;
                                    case FilterType.Query:
                                        var query = httpContext.Request.Query;
                                        foreach (RequestFilter item in filterResult)
                                        {
                                            var filterField = item.Field;
                                            var filterValues = item.Value;
                                            var queryFilterValue = query[filterField].ToString();
                                            if (queryFilterValue != "")
                                            {
                                                var allowed = filterValues.FirstOrDefault(x => x.ValueName == queryFilterValue.ToString());
                                                //Nếu tham số lọc không nằm trong các giá trị cho phép thì trả về unAuthorize
                                                if (allowed == null)
                                                {
                                                    await ReturnUnAuthorizeResponse(httpContext);
                                                }
                                            }
                                        }
                                        await _next.Invoke(httpContext);
                                        break;
                                    default:
                                        break;

                                }
                            }
                            else
                            {
                                await _next.Invoke(httpContext);
                            }
                        }
                        else
                        {
                            await ReturnUnAuthorizeResponse(httpContext);
                        }
                    }
                    else
                    {
                        await ReturnUnAuthorizeResponse(httpContext);
                    }

                }
                else
                {
                    await ReturnUnAuthorizeResponse(httpContext);
                }
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }

        private async Task ReturnUnAuthorizeResponse(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorize");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizeMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizeMiddleware>();
        }
    }
}
