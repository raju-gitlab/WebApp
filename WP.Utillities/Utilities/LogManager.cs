using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Utillities.Utilities
{
    public static class LogManager
    {
        public async static Task Log(Exception ex)
        {
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            string query = "";
            /*using(MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                await con.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.
                }
            }*/
        }
    }
}
