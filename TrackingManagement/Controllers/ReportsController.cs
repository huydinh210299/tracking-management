using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IReportService _reportService;
        private IConverter _converter;
        readonly string[] ReportType = { "TH", "NKHD", "TDHDX", "KMTX", "SKCB" };

        public ReportsController(IReportService reportService, IConverter converter)
        {
            _reportService = reportService;
            _converter = converter;
        }

        [HttpGet]
        [Route("car")]
        public IActionResult getCarReport([FromQuery] ReportFilter reportFilter)
        {
            if(reportFilter.Type == 0)
            {
                List<DailyCar> rs = _reportService.getDailyCarReport(reportFilter);
                BaseResponse<List<DailyCar>> res = new BaseResponse<List<DailyCar>>(rs);
                return Ok(res);
            }
            else if(reportFilter.Type == 3)
            {
                List<DailyKmCar> rs = _reportService.getDailyKmCarReport(reportFilter);
                BaseResponse<List<DailyKmCar>> res = new BaseResponse<List<DailyKmCar>>(rs);
                return Ok(res);
            }
            else
            {
                return Ok();
            }
        }
        
        [HttpPost]
        [Route("")]
        public IActionResult createReport([FromBody] CreateReport createReport)
        {
            DateTime today = DateTime.Now;
            string reportName = $"BC{ReportType[createReport.Type]}_{createReport.Obj}_{today.Day}{today.Month}{today.Year}_{today.Hour}{today.Minute}.pdf";
            var htmlContent = _reportService.getHtmlString(createReport);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Car Report",
                Out = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "reports", reportName)
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                // add file:/// prepend to fix dinktopdf error
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(@"file:///",Directory.GetCurrentDirectory(), "wwwroot", "css", "report.css") },
                FooterSettings = { FontName = "Arial", FontSize = 9, Center = "[page]/[toPage]" }
            };

            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            _converter.Convert(pdf);
            return Ok(new BaseResponse<string>(reportName));
        }
    }
}
