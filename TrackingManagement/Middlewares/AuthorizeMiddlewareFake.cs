using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace TrackingManagement.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthorizeMiddlewareFake
    {
        private readonly RequestDelegate _next;
        private JwtHandler jwtHandler = new JwtHandler();
        private readonly IConfiguration _config;

        public AuthorizeMiddlewareFake(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            BKContext _db = httpContext.RequestServices.GetService<BKContext>();
            var allowPath = _config.GetSection("AllowPath").Get<List<string>>();

            // get route Url
            var route = (httpContext.Features.Get<IEndpointFeature>()?.Endpoint as RouteEndpoint)?.RoutePattern.RawText;
            if(route != null)
            {
                route = "/" + route.ToLower();

                //check if current path in allowPath -> dont have to authorization
                var allowCurrentPath = allowPath.FirstOrDefault(value => value == route);

                if (allowCurrentPath == null)
                {
                    var secretKey = _config.GetSection("JwtSetting:SecretKey").Get<string>();
                    var token = String.IsNullOrEmpty(httpContext.Request.Headers["x-access-token"]) ? "" : (string)httpContext.Request.Headers["x-access-token"];
                    var decodeToken = jwtHandler.ValidateJwtToken(token, secretKey);

                    // Get scopeId, userId
                    var userId = decodeToken.Item1;
                    var scopeId = decodeToken.Item2;

                    var userUnits = _db.UserUnits.Where(item => item.UserId == userId).Select(item => (int)item.UnitId).ToList();
                    //var userUnits = userService.getUserUnit(userId);

                    // Thêm thông tin userId và scopeId vào httpContext
                    httpContext.Items["UID"] = userId;
                    httpContext.Items["scopeId"] = scopeId;
                    httpContext.Items["unitIds"] = userUnits;
                    await _next.Invoke(httpContext);
                }
                else
                {
                    await _next.Invoke(httpContext);
                }
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthorizeMiddlewareFakeExtensions
    {
        public static IApplicationBuilder UseAuthorizeMiddlewareFake(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizeMiddlewareFake>();
        }
    }
}
