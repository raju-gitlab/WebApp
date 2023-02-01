﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace WP.Repository.Classes
{
    public class PagesRepository : IPagesRepository
    {
        #region Params
        public string UUID { get; set; }
        public string userid { get; set; }
        #endregion

        public async Task<bool> IsValid(string pageName)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select EXISTS (select id from pages where pagename = '@pagename')";

                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@pagename", pageName));
                        if(Convert.ToInt32(await cmd.ExecuteScalarAsync()) == 1)
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

        public async Task<string> CreatePage(PageModel page)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string q1 = "select Id as UserId from usertbl where UserGuid = @userId";
                string query = "INSERT INTO pages(OwnerId, PageName, PageDescription, ProfileImagePath, Subscribers, LikesCount, CreatedDate, ModifiedDate, PageUUID, IsActived, IsBlocked) VALUES (@OwnerId, @PageName, @PageDescription, @ProfileImagePath, @Subscribers, @LikesCount,@CreatedDate, @ModifiedDate, @PageUUID, @IsActived, @IsBlocked)";
                
                using (MySqlConnection con = new MySqlConnection(ConnectionString)) 
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(q1, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@userid", page.OwnerId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if(rdr.Read())
                        {
                            userid = rdr["UserId"].ToString();
                            await con.CloseAsync();
                            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                            {
                                await connection.OpenAsync();
                                UUID = Guid.NewGuid().ToString();
                                using (MySqlCommand commmand = new MySqlCommand(query, connection))
                                {
                                    commmand.Parameters.AddWithValue("@OwnerId", Convert.ToInt32(userid));
                                    commmand.Parameters.AddWithValue("@PageName", page.PageName);
                                    commmand.Parameters.AddWithValue("@PageDescription", page.PageDescription);
                                    commmand.Parameters.AddWithValue("@ProfileImagePath", page.ProfileImagePath);
                                    commmand.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                                    commmand.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);
                                    commmand.Parameters.AddWithValue("@PageUUID", UUID);
                                    commmand.Parameters.AddWithValue("@Subscribers", 0);
                                    commmand.Parameters.AddWithValue("@LikesCount", 0);
                                    commmand.Parameters.AddWithValue("@IsActived", 1);
                                    commmand.Parameters.AddWithValue("@IsBlocked", 0);

                                    if(await commmand.ExecuteNonQueryAsync() > 0)
                                    {
                                        return UUID;
                                    }
                                    else
                                    {
                                        return null;
                                    }
                                }
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception exs)
            {

                throw;
            }
        }

        public async Task<string> ModifyPage(PageModifyModel page)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "UPDATE pages SET PageName=@PageName, PageDescription=@PageDescription, ProfileImagePath=@ProfileImagePath, ModifiedDate=@ModifiedDate, IsActivated=@IsActivated where PageUUID=@PageId";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PageName", page.PageName);
                        cmd.Parameters.AddWithValue("@PageDescription", page.PageDescription);
                        cmd.Parameters.AddWithValue("@ProfileImagePath", page.ProfileImagePath);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@IsActivated", page.IsActivated);
                        cmd.Parameters.Add(new MySqlParameter("@PageId", page.PageUUID));

                        if(Convert.ToInt32(cmd.ExecuteNonQueryAsync()) > 0)
                        {
                            await con.CloseAsync();
                            return page.PageUUID;
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
                throw;
            }
        }
    }
}