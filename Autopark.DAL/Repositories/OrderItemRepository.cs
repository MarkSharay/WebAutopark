using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;

namespace Autopark.DAL.Repositories
{
    public class OrderItemRepository:IRepository<OrderItem>
    {
        string connectionString = null; //make it private readonly. No need to initialize here
        public OrderItemRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<IEnumerable<OrderItem>> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<OrderItem>("SELECT * FROM OrderItems");
            }
        }

        public async Task<OrderItem> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryFirstAsync<OrderItem>("SELECT * FROM OrderItems WHERE OrderItemId = @id", new { id });
            }
        }

        public async Task Create(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO OrderItems " + //you can use '@' instead of concatenation
                    "(OrderId, ComponentId, Quantity)" +
                    " VALUES(@OrderId, @ComponentId, @Quantity)";
                await db.ExecuteAsync(sqlQuery, orderItem);
            }
        }

        public async Task Update(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Components SET OrderID = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity" + //you can use '@' instead of concatenation
                    "WHERE OrderItemId = @OrderItemId";
                await db.ExecuteAsync(sqlQuery, orderItem);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM OrderItems WHERE OrderItemId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }
}
