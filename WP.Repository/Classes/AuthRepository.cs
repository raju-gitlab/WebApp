﻿using MySql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Utillities.Encryption;
using WP.Utillities.Utilities;

namespace WP.Repository.Classes
{
    public class AuthRepository : IAuthRepository
    {
        #region Parameters
        public string userName{ get; set; }
        #endregion

        #region GET
        #region IsValid
        public async Task<bool> IsValid(string EmailId)
        {
            try
            {
                string query = "SELECT EXISTS (select Id from usertbl where Email = @emailid)";
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
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

        #endregion

        #region Login
        public async Task<bool> Login(string username, string password)
        {
            try
            {
                string query = "SELECT EXISTS (select Id from usertbl where FirstName = @fname)";
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@fname", username));
                        if (Convert.ToInt32(await cmd.ExecuteScalarAsync()) == 1)
                        {
                            await con.CloseAsync();
                            return true;
                        }
                        else
                        {
                            await con.CloseAsync();
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
        #endregion

        #region POST
        #region Register
        public async Task<string> Register(RegisterModel auth)
        {
            try
            {
                string salt = Encryption.getSalt();
                string pass = Encryption.getHash(salt+auth.Password);
                userName = (auth.Email.Split('@')[0].Length < 5) ? auth.Email.Split('@')[0] + "_" + Guid.NewGuid().ToString().Replace('-', 'm').Substring(0, 5) : auth.Email.Split('@')[0].Substring(0, 5) + "_" + Guid.NewGuid().ToString().Replace('-', 'm').Substring(0, 5);
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = Convert.ToString("insert into usertbl(FirstName, LastName, UserName, Email, Password, PasswordSalt, ContactNumber, IsVerified, CreationDate, ModifiedDate, UserGuid, UserType)" +
                "values(@FirstName, @LastName, @UserName, @Email, @Password, @PasswordSalt, @ContactNumber, @IsVerified, @CreationDate, @ModifiedDate, @UUID, @UserType)");
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@FirstName", auth.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", auth.LastName);
                        cmd.Parameters.AddWithValue("@UserName", userName);
                        cmd.Parameters.AddWithValue("@Email", auth.Email);
                        cmd.Parameters.AddWithValue("@Password", pass);
                        cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                        cmd.Parameters.AddWithValue("@IsVerified", 0);
                        cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@UUID", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@UserType", 1);

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
                await LogManager.Log(ex);
                return null;
            }
        }
        #endregion
        #endregion

        #region PUT
        #region UpdatePassword
        public async Task<bool> UpdatePassword(AuthModifyModel authModify)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                string PasswordHash = "", PasswordSalt = "";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("", PasswordHash);
                        cmd.Parameters.AddWithValue("", PasswordSalt);
                        cmd.Parameters.AddWithValue("", DateTime.UtcNow);
                        cmd.Parameters.Add(new MySqlParameter("", authModify.UserId));
                        if(await cmd.ExecuteNonQueryAsync() > 0)
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
                await LogManager.Log(ex);
                throw ex;
            }
        }
        #endregion

        #region UpdateAccountDeatails
        public async Task<bool> UpdateAccountDeatails(AccountModifyModel accountModify)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("", accountModify.ContactNumber);
                        cmd.Parameters.AddWithValue("", accountModify.FirstName);
                        cmd.Parameters.AddWithValue("", accountModify.LastName);
                        cmd.Parameters.AddWithValue("", accountModify.UserName);
                        cmd.Parameters.AddWithValue("@ModifyDate", DateTime.UtcNow);
                        cmd.Parameters.Add(new MySqlParameter("", accountModify.UserId));
                        if (await cmd.ExecuteNonQueryAsync() > 0)
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
                await LogManager.Log(ex);
                throw;
            }
        }
        #endregion

        #region UpdateProfileImage(Only)

        #endregion
        #endregion
    }
}
