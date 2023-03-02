using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Utilities;
using WP.Repository.Interfaces.Misc;
using WP.Utillities.Utilities;

namespace WP.Repository.Classes.Misc
{
    public class MiscRepository : IMiscRepository
    {
        #region Privacies list
        public async Task<List<PrivacyModel>> ListPrivacies()
        {
            try
            {
                List<PrivacyModel> privacytypes = new List<PrivacyModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select PrivacyType, PrivacyUUID from privacycategory";
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            privacytypes.Add(new PrivacyModel
                            {
                                PrivacyType = rdr["PrivacyType"].ToString(),
                                PrivacyUUID = rdr["PrivacyUUID"].ToString()
                            });
                        }
                    }
                    await connection.CloseAsync();
                }
                return privacytypes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region Categories list
        public async Task<List<CategoryModel>> ListCategories()
        {
            try
            {
                List<CategoryModel> categorytype = new List<CategoryModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select CategoryName, CategoryUUID from categories";
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            categorytype.Add(new CategoryModel
                            {
                                CategoryName = rdr["CategoryName"].ToString(),
                                CategoryUUID = rdr["CategoryUUID"].ToString()
                            });
                        }
                    }
                    await connection.CloseAsync();
                }
                return categorytype;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        #endregion

        #region Get category by id
        public async Task<int> GetCategoryId(string CategoryId)
        {
            int categoryid = -1;
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"].ToString();
                string query = "select Id as CategoryId from categories where CategoryUUID = @categoryid";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@categoryid", CategoryId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            categoryid = Convert.ToInt32(rdr["CategoryId"].ToString());
                        }
                    }
                    await con.CloseAsync();
                }
                return categoryid;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get privacy by id
        public async Task<int> GetPrivacyId(string PrivacyId)
        {
            int privacyid = -1;
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["Connectionstring"].ToString();
                string query = "select Id as PrivacyId from privacycategory where PrivacyUUID = @privacyid";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@privacyid", PrivacyId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            privacyid = Convert.ToInt32(rdr["PrivacyId"].ToString());
                        }
                    }
                    await con.CloseAsync();
                }
                return privacyid;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Get user id
        public async Task<int> GetUserId(string UserId)
        {
            int userid = -1;
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select Id as UserId from usertbl where UserGuid = @userid";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            userid = Convert.ToInt32(rdr["UserId"].ToString());
                        }
                    }
                }
                return userid;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Get page by id
        public async Task<int> GetPageId(string Pageid)
        {
            try
            {
                int pageid = -1;
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select Id as PageId from pages where PageUUID = @pageid";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@pageid", Pageid));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            pageid = Convert.ToInt32(rdr["PageId"].ToString());
                        }
                    }
                }
                return pageid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListTags
        public int MyProperty { get; set; }

        public async Task<List<string>> ListTags()
        {
            try
            {
                List<string> tags = new List<string>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select tagename as Tagname from tags";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
                        {
                            tags.Add(rdr["TagName"].ToString());
                        }
                    }
                    await con.CloseAsync();
                }
                return tags;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Add new tag
        public async Task<bool> Addtag(string[] tags)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "insert into tags(tagename, usedcount) values";
                StringBuilder sCommand = new StringBuilder(query);
                List<string> Rows = new List<string>();
                for (int i = 0; i < tags.Length; i++)
                {
                    Rows.Add(string.Format("('{0}','{1}')", MySqlHelper.EscapeString(tags[i].ToString()), MySqlHelper.EscapeString("0")));
                }
                sCommand.Append(string.Join(",", Rows));
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(sCommand.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (await cmd.ExecuteNonQueryAsync() > 0)
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
                await LogManager.Log(ex);
                throw ex; 
            }
        }
        #endregion
    }
}
