using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Autopark.DAL.Repositories
{
    internal class ComponentRepository:IRepository<Component>
    {
        string connectionString = null;
        public ComponentRepository(string conn)
        {
            connectionString = conn;
        }
        public List<Component> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Component>("SELECT * FROM Components").ToList();
            }
        }

        public Component Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Component>("SELECT * FROM Components WHERE ComponentId = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Component component)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Components " +
                    "(Name)" +
                    " VALUES(@Name)";
                db.Execute(sqlQuery, component);
            }
        }

        public void Update(Component component)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Components SET Name = @Name" +
                    "WHERE ComponentId = @ComponentId";
                db.Execute(sqlQuery, component);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Components WHERE ComponentId = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
