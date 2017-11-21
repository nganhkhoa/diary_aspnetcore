using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace diary.Models
{
    public class EntryContext
    {

        public string ConnectionString { get; set; }
        public EntryContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        public List<Entry> GetAllEntry()
        {
            List<Entry> entryList = new List<Entry>();
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM entry", conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {                     
                        entryList.Add(new Entry()
                        {                        
                            ID = reader.GetInt32(0),
                            Content = reader.GetString(1),
                            Date = reader.GetDateTime(2),                                                   
                        });
                    }
                }
                 conn.Close();
             }
            return entryList;
        }



}
}
