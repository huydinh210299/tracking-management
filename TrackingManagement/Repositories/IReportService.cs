using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface IReportService
    {
        public List<DailyCar> getDailyCarReport(ReportFilter reportFilter);
        public List<DailyKmCar> getDailyKmCarReport(ReportFilter reportFilter);
        public string getHtmlString(CreateReport createReport);
    }
}
