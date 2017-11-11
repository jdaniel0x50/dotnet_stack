using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using message_wall.Models;

namespace message_wall.Factory
{
    public class CommentFactory : IFactory<User>
    {
        // private readonly IOptions<MySqlOptions> MySqlConfig;
        private string connectionString;
        public CommentFactory()
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

        public void Add(int user_id, int msg_id, CommentViewModel NewComment)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "INSERT INTO comments ";
                query += "(user_id, message_id, comment) ";
                query += $"VALUES('{user_id}', '{msg_id}', '{NewComment.comment}')";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public IEnumerable<CommentWithUser> FindAllComments()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                string query = "SELECT comments.id AS id, comments.user_id AS user_id, ";
                query += "comments.message_id AS message_id, comments.comment, ";
                query += "comments.created_at AS created_at, ";
                query += "comments.updated_at AS updated_at, ";
                query += "users.username AS username, users.first_name AS first_name ";
                query += "FROM comments LEFT JOIN users on comments.user_id = users.id ";
                query += "ORDER BY comments.created_at ASC";
                return dbConnection.Query<CommentWithUser>(query);
            }
        }

        public Comment FindById(int cmmnt_id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Comment>($"SELECT * FROM comments WHERE id = {cmmnt_id}").FirstOrDefault();
            }
        }

        public void Update(int cmmnt_id, CommentViewModel UpdateComment)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "UPDATE comments ";
                query += $"SET comment = {UpdateComment.comment}) ";
                query += $"WHERE id = {cmmnt_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }

        public void Delete(int cmmnt_id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query = "DELETE FROM comments ";
                query += $"WHERE id = {cmmnt_id}";
                dbConnection.Open();
                dbConnection.Execute(query);
            }
        }
    }
}
