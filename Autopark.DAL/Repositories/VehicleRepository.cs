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
        public async Task<IEnumerable<Vehicle>> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<Vehicle>("SELECT * FROM Vehicles");
            }
        }

        public async Task<Vehicle> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryFirstAsync<Vehicle>("SELECT * FROM Vehicles WHERE VehicleId = @id", new { id });
            }
        }

        public async Task Create(Vehicle vehicle)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Vehicles " +
                    "(VehicleTypeId, Model, RegistrationNumber, Weight, Year, Mileage, Color, Volume, FuelConsuption)" +
                    " VALUES(@VehicleTypeId, @Model, @RegistrationNumber, @Weight, @Year, @Mileage, @Color, @Volume, @FuelConsuption)";
                await db.ExecuteAsync(sqlQuery, vehicle);
            }
        }

        public async Task Update(Vehicle vehicle)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Users SET VehicleTypeId = @VehicleTypeId, Model = @Model, RegistrationNumber = @RegistrationNumber," +
                    " Weight = @Weight, Year = @Year, Mileage = @Mileage, Color = @Color, Volume = @Volume, FuelConsuption = @FuelConsuption " +
                    "WHERE VehicleId = @VehicleId";
                await db.ExecuteAsync(sqlQuery, vehicle);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Vehicles WHERE Id = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }

}
