using Microsoft.Data.SqlClient;
using System.Data;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Entities;
using Dapper;

namespace Autopark.DAL.Repositories
{
    public class VehicleTypeRepository:IRepository<VehicleType>
    {
        string connectionString = null;
        public VehicleTypeRepository(string conn)
        {
            connectionString = conn;
        }
        public List<VehicleType> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<VehicleType>("SELECT * FROM VehicleTypes").ToList();
            }
        }

        public VehicleType Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<VehicleType>("SELECT * FROM VehicleTypes WHERE VehicleTypeId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(VehicleType vehicleType)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO VehicleTypes " +
                    "(TypeName, TaxCoefficient)" +
                    " VALUES(@TypeName, @TaxCoefficient)";
                db.Execute(sqlQuery, vehicleType);
            }
        }

        public void Update(VehicleType vehicleType)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE VehicleTypes SET TypeName = @TypeName, TaxCoefficient = @TaxCoefficient " +
                    "WHERE VehicleTypeId = @VehicleTypeId";
                db.Execute(sqlQuery, vehicleType);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM VehicleTypes WHERE VehicleTypeId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
