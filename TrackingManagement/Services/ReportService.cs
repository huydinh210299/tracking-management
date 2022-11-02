using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;

namespace TrackingManagement.Services
{
    public class ReportService : IReportService
    {
        private readonly BKContext _db;

        public ReportService(BKContext db)
        {
            _db = db;
        }

        public string getHtmlString(CreateReport createReport)
        {
            var html = new StringBuilder();
            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "BankLogo.png");
            html.Append(@"
                        <html>
                            <head></head>
                            <body>
                                <div class='flex-space-between'>
                                    <h4>NGÂN HÀNG TNHH MỘT THÀNH VIÊN XYZ BANK</h4>
                                    <image height=50 width=180 src='{0}' class='branch-logo'/>
                                </div>
                                <h3 class='report-title'>BÁO CÁO HOẠT ĐỘNG XE CHUYÊN DÙNG</h3>
                        ");
            html.Append($"<p class='date-fromto'>Từ ngày: {createReport.BeginDate} đến ngày: {createReport.EndDate}</p>");
            html.Append($"<p class='export-date'>Ngày xuất báo cáo: {DateTime.Now.ToString("dd/MM/yyyy")}</p>");
            html.Append(@"<table>");
            html.Append(createReport.ReportBody);
            html.Append(@"
                                </table>
                            </body>
                    </html>   
                    ");
            var stringHtml = html.ToString();
            // add file:/// prepend to fix dinktopdf error
            stringHtml = string.Format(stringHtml, @"file:///" + logoPath);
            return stringHtml;
        }

        public List<DailyCar> getDailyCarReport(ReportFilter reportFilter)
        {
            var carIds = new StringBuilder();
            carIds.AppendFormat("{0}", reportFilter.ObjectList[0]);
            for (int i = 1; i < reportFilter.ObjectList.Count; i++)
                carIds.AppendFormat(", {0}", reportFilter.ObjectList[i]);
            var sql = @"SELECT * FROM DaiLyCar 
                        Where ReportTime >= '{0}' AND ReportTime <= '{1}'
                        AND CarId IN ({2})";
            sql = string.Format(sql, reportFilter.BeginDate.ToString("yyyy-MM-dd ") + "00:00:00", reportFilter.EndDate.ToString("yyyy-MM-dd ") + "00:00:00", carIds);
            var query = _db.DailyCars.FromSqlRaw(sql);
            List<DailyCar> rs = query.ToList();
            return rs;
        }

        public List<DailyKmCar> getDailyKmCarReport(ReportFilter reportFilter)
        {
            var carIds = new StringBuilder();
            carIds.AppendFormat("{0}", reportFilter.ObjectList[0]);
            for (int i = 1; i < reportFilter.ObjectList.Count; i++)
                carIds.AppendFormat(", {0}", reportFilter.ObjectList[i]);
            var sql = @"SELECT * FROM DailyKmCar 
                        Where ReportTime >= '{0}' AND ReportTime <= '{1}'
                        AND CarId IN ({2})";
            sql = string.Format(sql, reportFilter.BeginDate.ToString("yyyy-MM-dd ") + "00:00:00", reportFilter.EndDate.ToString("yyyy-MM-dd ") + "00:00:00", carIds);
            var query = _db.DailyKmCars.FromSqlRaw(sql);
            List<DailyKmCar> rs = query.ToList();
            return rs;
        }
    }
}
