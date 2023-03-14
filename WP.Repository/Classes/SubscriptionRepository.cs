using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Utillities.Utilities;

namespace WP.Repository.Classes
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        #region Post
        #region Subscribe an Page
        public async Task<bool> SubscribePage(SubscriptionModel subscription)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "INSERT into UserPageFollowtbl (UserId, PageId, CreatedOn, ModifiedOn)" +
                    " values" +
                    " ((select Id from Usertbbbl where UserGuid = @Userid), (select Id from Pages where PageUUID = @Pageid), @CreatedOn, ModifiedOn)";
                using(MySqlConnection con  = new MySqlConnection(ConnectionString)) 
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con)) 
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@Userid", subscription.UserUUID));
                        cmd.Parameters.Add(new MySqlParameter("@Pageid", subscription.PageUUID));
                        cmd.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@ModifiedDate", null);
                        if(Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
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
                return false;
            }
        }
        #endregion
        #endregion

        #region Put
        #region RemoveSubscribePage
        public async Task<bool> RemoveSubscribePage(SubscriptionModel subscription)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "delete from UserPageFollowtbl" +
                    " where UserId = (select Id from Usertbbbl where UserGuid = @Userid) and" +
                    " PageId = (select Id from Pages where PageUUID = @Pageid)";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@Userid", subscription.UserUUID));
                        cmd.Parameters.Add(new MySqlParameter("@Pageid", subscription.PageUUID));
                        if (Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
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
                return false;
            }
        }
        #endregion
        #endregion
    }
}
