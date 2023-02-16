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

namespace WP.Repository.Classes.Misc
{
    public class MiscRepository : IMiscRepository
    {
        public async Task<List<PrivacyModel>> ListPrivacies()
        {
            try
            {
                List<PrivacyModel> privacytypes = new List<PrivacyModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select PrivacyType, PrivacyUUID from privacycategory";
                using(MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    await connection.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
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
    }
}
