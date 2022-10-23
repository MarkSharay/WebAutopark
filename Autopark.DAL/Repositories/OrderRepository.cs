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
        public List<Order> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Order>("SELECT * FROM Orders").ToList();
            }
        }

        public Order Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Order>("SELECT * FROM Orders WHERE OrderId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Orders " +
                    "(Date)" +
                    " VALUES(@Date)";
                db.Execute(sqlQuery, order);
            }
        }

        public void Update(Order order)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Orders SET Date = @Date" +
                    "WHERE OrderId = @OrderId";
                db.Execute(sqlQuery, order);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Orders WHERE OrderId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
