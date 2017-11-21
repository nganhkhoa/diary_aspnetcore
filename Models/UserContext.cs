using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace diary.Models
{
    public class UserContext
    {

        public string ConnectionString { get; set; }
        public UserContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        public List<User> GetAllUser()
        {
            List<User> userList = new List<User>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM user", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userList.Add(new User()
                        {
                            Id = reader.GetString(1).ToString(),
                            UserName = reader.GetString(18).ToString(),
                            Email = reader.GetString(5).ToString(),
                            FirstName = reader.GetString(7).ToString(),
                            LastName = reader.GetString(8).ToString(),
                            Brithday = reader.GetDateTime(3),
                        });
                    }       
                }
                conn.Close();
            }
            return userList;
        }
    }
}
