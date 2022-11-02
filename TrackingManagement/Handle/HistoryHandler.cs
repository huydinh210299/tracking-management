using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Handle
{
    public class HistoryHandler
    {
        public static KmCar GetKmCarStatistical(List<History> histories)
        {
            KmCar rs = new KmCar();
            if (histories.Count != 0)
            {
                List<Coordinate> points = new List<Coordinate>();
                List<int> velocityList = new List<int>();
                foreach (History history in histories)
                {
                    Coordinate point = new Coordinate((double)history.GpsLat, (double)history.GpsLon);
                    points.Add(point);
                    velocityList.Add((int)history.GpsVelocity);
                }
                double totalDistance = 0;
                for (var i = 0; i < points.Count - 1; i++)
                {
                    // calculate distance in miles
                    double distance = GeoCalculator.GetDistance(points[i], points[i + 1], 1);
                    totalDistance += distance;
                }
                rs.AverageSpeed = Math.Round(velocityList.Average(), 1);
                rs.MaximumSpeed = velocityList.Max();
                rs.MinimumSpeed = velocityList.Min();
                rs.Distance = Math.Round(totalDistance * 1.609344, 1);
            }
            return rs;
        }
    }
}
