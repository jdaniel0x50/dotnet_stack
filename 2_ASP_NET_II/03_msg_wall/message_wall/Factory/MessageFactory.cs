using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using message_wall.Models;

namespace message_wall.Factory
{
    public class MessageFactory : IFactory<User>
    {
        // private readonly IOptions<MySqlOptions> MySqlConfig;
        private string connectionString;
        public MessageFactory()
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

        public void Add(int user_id, MessageViewModel NewMessage)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO messages ";
                query += "(user_id, message) ";
                query += $"VALUES({user_id}, '{NewMessage.message}')";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public IEnumerable<MessageWithUser> FindAllMessages()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT messages.id AS id, messages.user_id AS user_id, ";
                query += "messages.message AS message, messages.created_at AS created_at, ";
                query += "messages.updated_at AS updated_at, ";
                query += "users.username AS username, users.first_name AS first_name ";
                query += "FROM messages LEFT JOIN users on messages.user_id = users.id ";
                query += "ORDER BY messages.created_at DESC";
                return dbConnection.Query<MessageWithUser>(query);
            }
        }

        public Message FindById(int msg_id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Message>($"SELECT * FROM messages WHERE id = {msg_id}").FirstOrDefault();
            }
        }

        public void Update(int msg_id, MessageViewModel UpdateMessage)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE messages ";
                query += $"SET message = {UpdateMessage.message}) ";
                query += $"WHERE id = {msg_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public void Delete(int msg_id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "DELETE FROM messages ";
                query += $"WHERE id = {msg_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
    }
}
        