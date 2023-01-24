using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using WP.Model.Models;
using WP.Repository.Interfaces;

namespace WP.Repository.Classes
{
    public class AuthRepository : IAuthRepository
    {
        public bool Issuccess()
        {
            List<AuthModel> results = new List<AuthModel>(); 
            string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();

            var command = new MySqlCommand("SELECT * FROM mytbl;", connection);
            var reader = command.ExecuteReader();
            while(reader.Read())
            {
                results.Add(new AuthModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    FirstName = reader["FirstName"].ToString(),
                    LastName = reader["LastName"].ToString()
                });
            }
            return true;
        }
    }
}
