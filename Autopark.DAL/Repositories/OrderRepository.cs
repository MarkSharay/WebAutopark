using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using Autopark.DAL.Entities;
using Dapper;
using Autopark.DAL.Interfaces;

namespace Autopark.DAL.Repositories{
    public class OrderRepository:IRepository<Order>
    {
        string connectionString = null;
        public OrderRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<IEnumerable<Order>> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<Order>("SELECT * FROM Orders");
            }
        }

        public async Task<Order> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryFirstAsync<Order>("SELECT * FROM Orders WHERE OrderId = @id", new { id });
            }
        }


        public async Task Create(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Orders " +
                    "(VehicleId, Date)" +
                    " VALUES(@VehicleId, @Date)";
                await db.ExecuteAsync(sqlQuery, order);
            }
        }

        public async Task Update(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Orders SET Date = @Date" +
                    "WHERE OrderId = @OrderId";
                await db.ExecuteAsync(sqlQuery, order);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Orders WHERE OrderId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }
}
