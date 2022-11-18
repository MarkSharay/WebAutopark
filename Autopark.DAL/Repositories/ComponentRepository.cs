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
    public class ComponentRepository:IRepository<Component>
    {
        string connectionString = null;
        public ComponentRepository(string conn)
        {
            connectionString = conn;
        }
        public async Task<IEnumerable<Component>> GetList()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryAsync<Component>("SELECT * FROM Components");
            }
        }

        public async Task<Component> Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return await db.QueryFirstAsync<Component>("SELECT * FROM Components WHERE ComponentId = @id", new { id });
            }
        }

        public async Task Create(Component component)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"INSERT INTO Components 
                (Name) 
                VALUES(@Name)";
                await db.ExecuteAsync(sqlQuery, component);
            }
        }

        public async Task Update(Component component)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = @"UPDATE Components SET Name = @Name
                    WHERE ComponentId = @ComponentId";
                await db.ExecuteAsync(sqlQuery, component);
            }
        }

        public async Task Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Components WHERE ComponentId = @id";
                await db.ExecuteAsync(sqlQuery, new { id });
            }
        }
    }
}
