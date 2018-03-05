using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;

namespace Eatech.FleetManager.ApplicationCore.Services
{
    public class CarService : ICarService
    {
        private SQLiteConnection conn;
        public CarService()
        {
            conn = new SQLiteConnection("Data Source=Cardatabase;Version=3;", true);
            conn.Open();
            SQLiteCommand tablesql = new SQLiteCommand("CREATE TABLE IF NOT EXISTS" +
                " Cars(id TEXT,brand TEXT,model TEXT,inspection_date TEXT, engine_size INTEGER," +
                " engine_power INTEGER, license_number TEXT, modelyear INTEGER)", conn);
            tablesql.ExecuteNonQuery();
        }
        
        public async Task<List<Car>> GetAll()
        {
            List<Car> Carlist = new List<Car>();
            SQLiteCommand getsql = new SQLiteCommand("SELECT * FROM Cars ORDER BY id ASC", conn);
            SQLiteDataReader rows = getsql.ExecuteReader();

            while (rows.Read())
            {
                Car car = new Car
                {
                    Brand = rows.GetString(rows.GetOrdinal("brand")),
                    Model = rows.GetString(rows.GetOrdinal("model")),
                    Inspection_date = DateTime.Parse(rows.GetString(rows.GetOrdinal("inspection_date"))),
                    Engine_size = rows.GetInt32(rows.GetOrdinal("engine_size")),
                    Engine_power = rows.GetInt32(rows.GetOrdinal("engine_power")),
                    License_number = rows.GetString(rows.GetOrdinal("license_number")),
                    ModelYear = rows.GetInt32(rows.GetOrdinal("modelyear"))
                };
                car.Id = Guid.Parse(rows.GetString(rows.GetOrdinal("id")));
                Carlist.Add(car);
            }
            return await Task.FromResult(Carlist);
        }

        public async Task<Car> Get(Guid id)
        {
            SQLiteCommand getcarsql = new SQLiteCommand("SELECT * FROM Cars WHERE id = '" + id.ToString() + "'", conn);
            SQLiteDataReader row = getcarsql.ExecuteReader();
            if (row.Read()) {
                Car car = new Car
                {
                    Brand = row.GetString(row.GetOrdinal("brand")),
                    Model = row.GetString(row.GetOrdinal("model")),
                    Inspection_date = DateTime.Parse(row.GetString(row.GetOrdinal("inspection_date"))),
                    Engine_size = row.GetInt32(row.GetOrdinal("engine_size")),
                    Engine_power = row.GetInt32(row.GetOrdinal("engine_power")),
                    License_number = row.GetString(row.GetOrdinal("license_number")),
                    Id = Guid.Parse(row.GetString(row.GetOrdinal("id"))),
                    ModelYear = row.GetInt32(row.GetOrdinal("modelyear"))
                };
                return await Task.FromResult(car);
            }
            return await Task.FromResult<Car>(null);
        }
        public async Task<List<Car>> List(int min, int max, string brand, string model)
        {
            List<Car> Carlist = new List<Car>();
            SQLiteCommand getsql = new SQLiteCommand("SELECT * FROM Cars ORDER BY id ASC", conn);
            SQLiteDataReader rows = getsql.ExecuteReader();
            
            while (rows.Read())
            {
                bool condition1 = (min == 0) || (rows.GetInt32(rows.GetOrdinal("modelyear"))) >= min;
                bool condition2 = (max == 0) || (rows.GetInt32(rows.GetOrdinal("modelyear"))) <= max;
                bool condition3 = (brand == null) || (rows.GetString(rows.GetOrdinal("brand"))).Equals(brand);
                bool condition4 = (model == null) || (rows.GetString(rows.GetOrdinal("model"))).Equals(model);
                if (condition1 && condition2 && condition3 && condition4)
                {
                    Car car = new Car
                    {
                        Brand = rows.GetString(rows.GetOrdinal("brand")),
                        Model = rows.GetString(rows.GetOrdinal("model")),
                        Inspection_date = DateTime.Parse(rows.GetString(rows.GetOrdinal("inspection_date"))),
                        Engine_size = rows.GetInt32(rows.GetOrdinal("engine_size")),
                        Engine_power = rows.GetInt32(rows.GetOrdinal("engine_power")),
                        License_number = rows.GetString(rows.GetOrdinal("license_number")),
                        ModelYear = rows.GetInt32(rows.GetOrdinal("modelyear"))
                    };
                    car.Id = Guid.Parse(rows.GetString(rows.GetOrdinal("id")));
                    Carlist.Add(car);
                }
            }
            
            return await Task.FromResult(Carlist);
        }
        public Car Add(string brand, string model, string inspection_date, int engine_size,
            int engine_power, string license_number, int modelYear)
        {
            DateTime parsedDate;
            Car car = new Car();
            if (DateTime.TryParse(inspection_date, out parsedDate)) {
                car = new Car
                {
                    Id = Guid.NewGuid(),
                    Brand = brand,
                    Model = model,
                    Inspection_date = parsedDate,
                    Engine_size = engine_size,
                    Engine_power = engine_power,
                    License_number = license_number,
                    ModelYear = modelYear
                };
            }
            else
            {
                car = new Car
                {
                    Id = Guid.NewGuid(),
                    Brand = brand,
                    Model = model,
                    Inspection_date = DateTime.Now,
                    Engine_size = engine_size,
                    Engine_power = engine_power,
                    License_number = license_number,
                    ModelYear = modelYear
                };
                inspection_date = DateTime.Now.ToString();
            }
            SQLiteCommand sqladd = new SQLiteCommand("INSERT INTO Cars (id,brand,model,inspection_date," +
                "engine_size,engine_power,license_number,modelyear) VALUES ('" + (car.Id).ToString() +
                "','" + brand + "','" + model + "','" + inspection_date + "', '" + engine_size + "'," +
                " '" + engine_power + "', '" + license_number + "', '" + modelYear + "')", conn);
            sqladd.ExecuteNonQuery();
            return car;
        }
        public Car Update(Guid id, string brand,string model, string inspection_date, int engine_size,
            int engine_power, string license_number, int modelYear)
        {
            DateTime parsedDate;
            Car car = new Car();
            if (DateTime.TryParse(inspection_date, out parsedDate))
            {
                SQLiteCommand setcarsql = new SQLiteCommand("UPDATE Cars SET brand = '" + brand +
                    "', inspection_date = '" + inspection_date + "', model = '" + model + "', engine_power = '" + engine_power +
                    "', engine_size = '" + engine_size + "', license_number = '" + license_number + "'," +
                    " modelyear = '" + modelYear + "' WHERE id = '" + id.ToString() + "'", conn);
                if (setcarsql.ExecuteNonQuery() > 0)
                {
                    car = new Car
                    {
                        Id = id,
                        Brand = brand,
                        Model = model,
                        Inspection_date = parsedDate,
                        Engine_size = engine_size,
                        Engine_power = engine_power,
                        License_number = license_number,
                        ModelYear = modelYear
                    };
                    return car;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                SQLiteCommand setcarsql = new SQLiteCommand("UPDATE Cars SET brand = '" + brand +
                        "', inspection_date = '" + DateTime.Now.ToString() + "', model = '" + model + "', engine_power = '" + engine_power +
                        "', engine_size = '" + engine_size + "', license_number = '" + license_number + "'," +
                        " modelyear = '" + modelYear + "', WHERE id = '" + id.ToString() + "'", conn);
                if (setcarsql.ExecuteNonQuery() > 0)
                {
                    car = new Car
                    {
                        Id = id,
                        Brand = brand,
                        Model = model,
                        Inspection_date = DateTime.Now,
                        Engine_size = engine_size,
                        Engine_power = engine_power,
                        License_number = license_number,
                        ModelYear = modelYear
                    };
                    return car;
                }
                else
                {
                    return null;
                }
            }
        }
        public Car Delete(Guid id)
        {
            SQLiteCommand getcarsql = new SQLiteCommand("SELECT * FROM Cars WHERE id = '" + id.ToString() + "'", conn);
            SQLiteDataReader row = getcarsql.ExecuteReader();
            if ( row.Read())
            {
                Car car = new Car
                {
                    Brand = row.GetString(row.GetOrdinal("brand")),
                    Model = row.GetString(row.GetOrdinal("model")),
                    Inspection_date = DateTime.Parse(row.GetString(row.GetOrdinal("inspection_date"))),
                    Engine_size = row.GetInt32(row.GetOrdinal("engine_size")),
                    Engine_power = row.GetInt32(row.GetOrdinal("engine_power")),
                    License_number = row.GetString(row.GetOrdinal("license_number")),
                    Id = Guid.Parse(row.GetString(row.GetOrdinal("id"))),
                    ModelYear = row.GetInt32(row.GetOrdinal("modelyear"))
                };
                SQLiteCommand deletesql = new SQLiteCommand("DELETE FROM Cars WHERE id = '" + id.ToString() + "'", conn);
                deletesql.ExecuteNonQuery();
                return car;
            }
            return null;



        }
        public void Close()
        {
            conn.Close();
        }
        public void Clear()
        {
            SQLiteCommand cleartable = new SQLiteCommand("DELETE FROM Cars", conn);
            cleartable.ExecuteNonQuery();
        }
    }
}