using AutoFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;
using TrackingManagement.Repositories;
using TrackingManagement.Utils;

namespace TrackingManagement.Services
{
    public class CarService: ICarService
    {
        private readonly BKContext _db;

        public CarService(BKContext db)
        {
            _db = db;
        }

        public int createCar(Car car)
        {
            _db.Cars.Add(car);
            var rfid = _db.Rfids.Where(item => item.Id == car.RfidId).FirstOrDefault();
            if (rfid != null)
            {
                rfid.IsDistributed = true;
            }
            return _db.SaveChanges();
        }

        public async Task<PagedResponse<List<Car>>> getCars(PaginationFilter paginationFilter, CarFilter carFilter, List<int> unitIds)
        {
            var page = paginationFilter.Page;
            var record = paginationFilter.Record;
            var total = await _db.Cars
                        .Where(item => unitIds.Contains((int)item.UnitId))
                        .AutoFilter(carFilter).CountAsync();
            var query = _db.Cars
                            .Where(item => unitIds.Contains((int)item.UnitId))
                            .AutoFilter(carFilter)
                            .Include(item => item.Rfid)
                            .Include(item => item.Unit)
                            .Include(item => item.Driver);
            if(paginationFilter.Paging == true)
            {
                var queryRs = await query.Skip((page - 1) * record).Take(record).ToListAsync();
                return new PagedResponse<List<Car>>(queryRs, total);
            }
            else
            {
                var queryRs = await query.ToListAsync();
                return new PagedResponse<List<Car>>(queryRs, total);
            }
        }

        public int updateCar(UpdateCar updateCar, int carId)
        {
            var car = _db.Cars.Where(item => item.Id == carId).FirstOrDefault();
            if (updateCar.RfidId != null)
            {
                var newRfid = _db.Rfids.Where(item => item.Id == updateCar.RfidId).FirstOrDefault();
                if(car.RfidId != null)
                {
                    if (car.RfidId != updateCar.RfidId)
                    {

                        var currentRfid = _db.Rfids.Where(item => item.Id == car.RfidId).FirstOrDefault();
                        currentRfid.IsDistributed = false;
                    }
                }
                newRfid.IsDistributed = true;
            }
            var udpateRs = EntityUtils.updateRecord(car, updateCar);
            return _db.SaveChanges();
        }
    }
}
