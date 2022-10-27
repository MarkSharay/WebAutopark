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
        string connectionString = null;
        public OrderItemRepository(string conn)
        {
            connectionString = conn;
        }
        public List<OrderItem> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<OrderItem>("SELECT * FROM OrderItems").ToList();
            }
        }

        public OrderItem Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<OrderItem>("SELECT * FROM OrderItems WHERE OrderItemId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO OrderItems " +
                    "(OrderId, ComponentId, Quantity)" +
                    " VALUES(@OrderId, @ComponentId, @Quantity)";
                db.Execute(sqlQuery, orderItem);
            }
        }

        public void Update(OrderItem orderItem)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Components SET OrderID = @OrderId, ComponentId = @ComponentId, Quantity = @Quantity" +
                    "WHERE OrderItemId = @OrderItemId";
                db.Execute(sqlQuery, orderItem);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM OrderItems WHERE OrderItemId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
