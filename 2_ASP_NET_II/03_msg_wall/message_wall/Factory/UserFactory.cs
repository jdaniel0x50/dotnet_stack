using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using message_wall.Models;

namespace message_wall.Factory
{
    public class UserFactory : IFactory<User>
    {
        // private readonly IOptions<MySqlOptions> MySqlConfig;
        private string connectionString;
        public UserFactory()
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

        public void Add(UserRegViewModel NewUser)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO users ";
                query += "(username, first_name, last_name, email, birthdate, password) ";
                query += $"VALUES('{NewUser.Username}', '{NewUser.FirstName}', '{NewUser.LastName}', '{NewUser.Email}', STR_TO_DATE('{NewUser.Birthdate}', '%m/%d/%Y %h:%i:%s %p'), '{NewUser.Password}')";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
        public IEnumerable<User> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>("SELECT * FROM users");
            }
        }
        public User FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>($"SELECT * FROM users WHERE id = {id}").FirstOrDefault();
            }
        }

        public User FindByEmail(string _email)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>($"SELECT * FROM users WHERE email = '{_email}'").FirstOrDefault();
            }
        }

        public User FindByUsername(string _username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>($"SELECT * FROM users WHERE username = '{_username}'").FirstOrDefault();
            }
        }

        public User FindByLogin(string _username, string _password)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<User>($"SELECT * FROM users WHERE BINARY username = '{_username}' AND BINARY password = '{_password}'").FirstOrDefault();
            }
        }
    }
}