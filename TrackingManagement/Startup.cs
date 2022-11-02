using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using TrackingManagement.Middlewares;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Services;
using TrackingManagement.Utils;

namespace TrackingManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            // Load library for pdf tool
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));
            //add PDF tool
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            //services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
            ); ;
            services.AddDbContext<BKContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("BKConStr")));

            // Add  scope
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IRFIDService, RFIDService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ITransactionPointService, TransactionPointService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ISegmentationService, SegmentationService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IOnlineService, OnlineService>();
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IReportService, ReportService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BKContext bKContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("ClientPermission");

            app.UseRouting();

            //app.UseAuthorizeMiddleware();
            app.UseAuthorizeMiddlewareFake();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
