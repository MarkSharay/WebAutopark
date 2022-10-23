using Microsoft.Data.SqlClient;
using System.Data;
using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Dapper;

namespace Autopark.DAL.Repositories
{
    public class VehicleRepository:IRepository<Vehicle>
    {
        string connectionString = null;
        public VehicleRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Vehicle> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Vehicle>("SELECT * FROM Vehicles").ToList();
            }
        }

        public Vehicle Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Vehicle>("SELECT * FROM Vehicles WHERE VehicleId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Vehicle vehicle)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Vehicles " +
                    "(Model, RegistrationNumber, Weight, Year, Mileage, Color, Volume, FuelConsuption)" +
                    " VALUES(@Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @Volume, @FuelConsuption)";
                db.Execute(sqlQuery, vehicle);
            }
        }

        public void Update(Vehicle vehicle)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET Model = @Model, RegistrationNumber = @RegistrationNumber," +
                    " Weight = @Weight, Year = @Year, Mileage = @Mileage, Color = @Color, Volume = @Volume, FuelConsuption = @FuelConsuption " +
                    "WHERE VehicleId = @VehicleId";
                db.Execute(sqlQuery, vehicle);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Vehicles WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }

}
