﻿using MySql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;

namespace WP.Repository.Classes
{
    public class AuthRepository : IAuthRepository
    {
        #region Parameters
        public string userName{ get; set; }
        #endregion

        #region GET
        public async Task<bool> IsValid(string EmailId)
        {
            try
            {
                string query = "SELECT EXISTS (select Id from usertbl where Email = @emailid)";
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@emailid", EmailId));
                        if (Convert.ToInt32(await cmd.ExecuteScalarAsync()) == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Login(string username, string password)
        {
            try
            {
                string query = "SELECT EXISTS (select Id from usertbl where FirstName = @fname)";
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@fname", username));
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 1)
                        {
                            con.CloseAsync();
                            return true;
                        }
                        else
                        {
                            con.CloseAsync();
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region POST
        public async Task<string> Register(AuthModel auth)
        {
            try
            {
                string s = "dd";
                userName = (auth.Email.Split('@')[0].Length < 5) ? auth.Email.Split('@')[0] + "_" + Guid.NewGuid().ToString().Replace('-', 'm').Substring(0, 5) : auth.Email.Split('@')[0].Substring(0, 5) + "_" + Guid.NewGuid().ToString().Replace('-', 'm').Substring(0, 5);
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = Convert.ToString("insert into usertbl(FirstName, LastName, UserName, Email, Password, PasswordSalt, ContactNumber, IsVerified, CreationDate, ModifiedDate, UserGuid)" +
                "values(@FirstName, @LastName, @UserName, @Email, @Password, @PasswordSalt, @ContactNumber, @IsVerified, @CreationDate, @ModifiedDate, @UUID)");
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        //cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@FirstName", auth.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", auth.LastName);
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Email", auth.Email);
                        cmd.Parameters.AddWithValue("@Password", auth.Password);
                        cmd.Parameters.AddWithValue("@PasswordSalt", auth.PasswordSalt);
                        cmd.Parameters.AddWithValue("@ContactNumber", auth.ContactNumber);
                        cmd.Parameters.AddWithValue("@IsVerified", auth.IsVerified);
                        cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UUID", Guid.NewGuid().ToString());
                        
                        if (await cmd.ExecuteNonQueryAsync() > 0)
                        {
                            await con.CloseAsync();
                            return userName;
                        }
                        else
                        {
                            await con.CloseAsync();
                            return null;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
