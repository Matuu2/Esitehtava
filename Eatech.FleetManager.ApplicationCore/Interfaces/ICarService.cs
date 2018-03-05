using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;

namespace Eatech.FleetManager.ApplicationCore.Interfaces
{
    public interface ICarService
    {
        Task<List<Car>> GetAll();

        Task<Car> Get(Guid id);

        Task<List<Car>> List(int min, int max, string brand, string model);
        Car Add(string brand, string model, string inspection_date, int engine_size,
            int engine_power, string license_number, int modelYear);
        Car Update(Guid id, string brand, string model, string inspection_date, int engine_size,
            int engine_power, string license_number, int modelYear);
        Car Delete(Guid id);
        void Close();
        void Clear();
    }
}