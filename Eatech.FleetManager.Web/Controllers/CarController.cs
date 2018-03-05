using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eatech.FleetManager.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        ///     Example HTTP GET: api/car
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<CarDto>> Get()
        {

            return (await _carService.GetAll()).Select(c => new CarDto
            {
                Id = c.Id,
                ModelYear = c.ModelYear,
                Brand = c.Brand,
                Model = c.Model,
                Engine_power = c.Engine_power,
                Engine_size = c.Engine_size,
                Inspection_date = c.Inspection_date,
                License_number = c.License_number

            });
        }

        /// <summary>
        ///     Example HTTP GET: api/car/570890e2-8007-4e5c-a8d6-c3f670d8a9be
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
          
            var car = await _carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Engine_power =car.Engine_power,
                Engine_size = car.Engine_size,
                Inspection_date = car.Inspection_date,
                License_number = car.License_number,
                ModelYear = car.ModelYear
            });
        }
        [HttpGet("list")]
        public async Task<IEnumerable<CarDto>> List(int min, int max, string brand, string model)
        {
            return (await _carService.List(min,max,brand,model)).Select(c => new CarDto
            {
                Id = c.Id,
                ModelYear = c.ModelYear,
                Brand = c.Brand,
                Model = c.Model,
                Engine_power = c.Engine_power,
                Engine_size = c.Engine_size,
                Inspection_date = c.Inspection_date,
                License_number = c.License_number

            });
        }
        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            Car car = _carService.Delete(id);
            if(car == null)
            {
                return NotFound();
            }
            return Ok(new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Engine_power = car.Engine_power,
                Engine_size = car.Engine_size,
                Inspection_date = car.Inspection_date,
                License_number = car.License_number,
                ModelYear = car.ModelYear
            });
        }
        [HttpGet("add")]
        public IActionResult Add(string brand,string model, string inspection_date,
            int engine_size,int engine_power,string license_number, int modelyear)
        {
            Car car = _carService.Add(brand,model, inspection_date, engine_size, engine_power, license_number, modelyear);
            return Ok(car);
        }
        [HttpGet("update/{id}")]
        public IActionResult Update(Guid id, string brand, string model, string inspection_date,
            int engine_size, int engine_power, string license_number, int modelyear)
        {
            Car car = _carService.Update(id, brand, model, inspection_date, engine_size, engine_power, license_number, modelyear);
            return Ok(car);
        }

        [HttpGet("clear")]
        public IActionResult Clear()
        {
            _carService.Clear();
            return Ok();
        }
    }
}