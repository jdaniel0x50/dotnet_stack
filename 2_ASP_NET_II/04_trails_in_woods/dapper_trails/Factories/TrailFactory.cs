using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using dapper_trails.Models;

namespace dapper_trails.Factory
{
    public class TrailFactory : IFactory<Trail>
    {
        // private readonly IOptions<MySqlOptions> MySqlConfig;
        private string connectionString;
        public TrailFactory()
        {
            //MySqlConfig = config;
            //connectionString = MySqlConfig.Value.ConnectionString;
            connectionString = "server=localhost;userid=root;password=root;port=3306;database=netcore_users;SslMode=None";
        }
        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(connectionString);
            }
        }

        public void Add(TrailAddViewModel NewTrail)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO trails ";
                query += "(name, description, length, elevation_change, longitude, latitude) ";
                query += "VALUES(@name, @description, @length, @elevation_change, @longitude, @latitude)";
                dbConnection.Open();
                dbConnection.Execute(query, NewTrail);
            }
        }
        public IEnumerable<Trail> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>("SELECT * FROM trails");
            }
        }
        public Trail FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Trail>($"SELECT * FROM trails WHERE id = {id}").FirstOrDefault();
            }
        }

    }
}