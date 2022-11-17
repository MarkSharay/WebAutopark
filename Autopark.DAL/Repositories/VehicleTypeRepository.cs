using Microsoft.Data.SqlClient;
using System.Data;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Entities;
using Dapper;
using System.Collections;

namespace Autopark.DAL.Repositories
{
    public class VehicleTypeRepository:IRepository<VehicleType>
    {
        string connectionString = null; //make it private readonly. No need to initialize here
        public VehicleTypeRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<IEnumerable<VehicleType>> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<VehicleType>("SELECT * FROM VehicleTypes");
            }
        }

        public async Task<VehicleType> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryFirstAsync<VehicleType>("SELECT * FROM VehicleTypes WHERE VehicleTypeId = @id", new { id });
            }
        }

        public async Task Create(VehicleType vehicleType)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO VehicleTypes " +
                    "(TypeName, TaxCoefficient)" +
                    " VALUES(@TypeName, @TaxCoefficient)";
                await db.ExecuteAsync(sqlQuery, vehicleType);
            }
        }

        public async Task Update(VehicleType vehicleType)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient " +
                    "WHERE VehicleTypeId = @VehicleTypeId";
                await db.ExecuteAsync(sqlQuery, vehicleType);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }
}
