using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface ICarService
    {
        public int createCar(Car car);
        public Task<PagedResponse<List<Car>>> getCars(PaginationFilter paginationFilter, CarFilter carFilter, List<int> unitIds);
        public int updateCar(UpdateCar updateCar, int carId);
    }
}
