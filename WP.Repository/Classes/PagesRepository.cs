using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using WP.Utillities.Utilities;
using System.CodeDom;

namespace WP.Repository.Classes
{
    public class PagesRepository : IPagesRepository
    {
        #region Params
        public string UUID { get; set; }
        public string userid { get; set; }
        #endregion

        #region Get

        #region MyRegion
        public async Task<List<RolesModel>> UserRoles()
        {
            try
            {
                List<RolesModel> roles = new List<RolesModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "SELECT RoleName, RoleId FROM roleslist";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
                        {
                            roles.Add(new RolesModel
                            {
                                RoleName = rdr["RoleName"].ToString(),
                                RoleId = rdr["RoleId"].ToString()
                            });
                        }

                        await con.CloseAsync();
                    }
                }
                return roles;
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                throw;
            }
        }
        #endregion

        #region Listallpages
        public async Task<List<PageModel>> ListPages()
        {
            try
            {
                List<PageModel> results = new List<PageModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            results.Add(new PageModel
                            {
                                PageName = (string.IsNullOrEmpty(reader["PageName"].ToString()) ? reader["PageDescription"].ToString() : "Unknown alias"),
                                PageDescription = (string.IsNullOrEmpty(reader["PageDescription"].ToString()) ? reader["PageDescription"].ToString() : "Unknown alias"),
                                //ProfileImagePath = (string.IsNullOrEmpty(reader["ProfileImagePath"].ToString()) ? reader["ProfileImagePath"].ToString() : "no img"),
                                PageUUID = (string.IsNullOrEmpty(reader["PageUUID"].ToString()) ? reader["PageUUID"].ToString() : null),
                                OwnerId = (string.IsNullOrEmpty(reader["OwnerId"].ToString()) ? reader["OwnerId"].ToString() : null),
                                CreatedDate = (string.IsNullOrEmpty(reader["CreatedDate"].ToString()) ? DateTime.Parse(reader["CreatedDate"].ToString()) : DateTime.Parse(null)),
                                ModifiedDate = (string.IsNullOrEmpty(reader["ModifiedDate"].ToString()) ? DateTime.Parse(reader["ModifiedDate"].ToString()) : DateTime.Parse(null)),
                                Subscribers = Convert.ToInt64(reader["Subscribers"].ToString()),
                                LikesCount = Convert.ToInt64(reader["LikeCount"].ToString()),
                                IsActivated = Convert.ToBoolean(reader["IsActivated"].ToString())
                            });
                        }
                    }

                    await con.CloseAsync();
                }

                if (results.Count != 0)
                {
                    return null;
                }
                else
                {
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region PageById
        public async Task<Tuple<PageModel, List<PostsViewModel>>> PageById(string PageId)
        {
            try
            {
                Tuple<PageModel, List<PostsViewModel>> result = new Tuple<PageModel, List<PostsViewModel>>(new PageModel(), new List<PostsViewModel>());
                List<PageModel> list = new List<PageModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    string query = "SELECT p.PageName, p.PageDescription, p.ProfileImagePath, p.PageUUID, c.CategoryName, p.CreatedDate,p.IsActived, p.LikesCount,p.Subscribers, pc.PrivacyType, p.IsBlocked" +
                    " FROM pages p" +
                    " inner join categories c on c.Id = p.PageType" +
                    " inner join privacycategory pc on pc.Id = p.Privacytype" +
                    " where p.PageUUID = @pageId";
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@pageId", PageId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            result.Item1.PageName = (!string.IsNullOrEmpty(rdr["PageName"].ToString()) ? rdr["PageName"].ToString() : null);
                            result.Item1.PageDescription = (!string.IsNullOrEmpty(rdr["PageDescription"].ToString()) ? rdr["PageDescription"].ToString() : null);
                            result.Item1.ProfileImagePath = (!string.IsNullOrEmpty(rdr["ProfileImagePath"].ToString()) ? rdr["ProfileImagePath"].ToString() : null);
                            result.Item1.PageUUID = (!string.IsNullOrEmpty(rdr["PageUUID"].ToString()) ? rdr["PageUUID"].ToString() : null);
                            result.Item1.CategoryType = (!string.IsNullOrEmpty(rdr["CategoryName"].ToString()) ? rdr["CategoryName"].ToString() : null);
                            result.Item1.CreatedDate = (!string.IsNullOrEmpty(rdr["CreatedDate"].ToString()) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.MinValue);
                            result.Item1.IsActivated = (!string.IsNullOrEmpty(rdr["IsActived"].ToString()) ? Convert.ToBoolean(rdr["IsActived"].ToString()) : false);
                            result.Item1.LikesCount = (!string.IsNullOrEmpty(rdr["LikesCount"].ToString()) ? Convert.ToInt64(rdr["LikesCount"].ToString()) : 0);
                            result.Item1.Subscribers = (!string.IsNullOrEmpty(rdr["Subscribers"].ToString()) ? Convert.ToInt64(rdr["Subscribers"].ToString()) : 0);
                            result.Item1.PrivacyType = (!string.IsNullOrEmpty(rdr["PrivacyType"].ToString()) ? Convert.ToString(rdr["PrivacyType"].ToString()) : null);
                            result.Item1.IsBlocked = (!string.IsNullOrEmpty(rdr["IsBlocked"].ToString()) ? Convert.ToBoolean(rdr["IsBlocked"].ToString()) : true);
                        }
                    }
                    await con.CloseAsync();
                    if (result.Item1 == null)
                    {
                        return null;
                    }
                    else
                    {
                        query = "SELECT p.PostTitle, p.PostDescription, c.CategoryName,  p.CreatedOn, p.LikeCount, p.DislikeCount, p.FilePath, p.PostUUID, p.UserId" +
                        " FROM posts p" +
                        " inner join categories c on c.Id = p.PostCategory" +
                        " where p.MediaVisibility != 2 and p.MediaVisibility != 3";
                        await con.OpenAsync();
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            //cmd.Parameters.Add(new MySqlParameter("@", PageId));
                            DbDataReader rdr = await cmd.ExecuteReaderAsync();
                            while (await rdr.ReadAsync())
                            {
                                result.Item2.Add(new PostsViewModel
                                {
                                    PostTitle = !string.IsNullOrEmpty(rdr["PostTitle"].ToString()) ? rdr["PostTitle"].ToString() : null,
                                    PostDescription = !string.IsNullOrEmpty(rdr["PostDescription"].ToString()) ? rdr["PostDescription"].ToString() : null,
                                    PostCategoryName = !string.IsNullOrEmpty(rdr["CategoryName"].ToString()) ? rdr["CategoryName"].ToString() : null,
                                    CreatedDate = !string.IsNullOrEmpty(rdr["CreatedOn"].ToString()) ? DateTime.Parse(rdr["CreatedOn"].ToString()) : DateTime.MinValue,
                                    LikeCount = !string.IsNullOrEmpty(rdr["LikeCount"].ToString()) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                    DislikeCount = !string.IsNullOrEmpty(rdr["DislikeCount"].ToString()) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                    FilePath = !string.IsNullOrEmpty(rdr["FilePath"].ToString()) ? rdr["FilePath"].ToString() : null,
                                    PostUUID = !string.IsNullOrEmpty(rdr["PostUUID"].ToString()) ? rdr["PostUUID"].ToString() : null,
                                    UserUUID = !string.IsNullOrEmpty(rdr["UserId"].ToString()) ? rdr["UserId"].ToString() : null
                                });
                            }
                            await con.CloseAsync();
                        }
                    }
                }
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ListPagesbyfilter
        public async Task<List<PageModel>> ListPagesbyfilter(string[] filters)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region GetPagesByUserId
        public async Task<List<PageCardModel>> GetPagesByUserId(string UserId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select ut.FirstName, Ut.lastName, p.PageName, p.PageDescription, p.LikesCount, p.subscribers, p.ProfileImagePath, p.PageUUID from pages p "
                + "inner join usertbl ut on ut.Id = p.OwnerId "
                + "where ut.UserGuid = @userid";
                List<PageCardModel> UserPages = new List<PageCardModel>();
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query,con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
                        {
                            UserPages.Add(new PageCardModel
                            {
                                PageName = (!string.IsNullOrEmpty(rdr["PageName"].ToString())) ? rdr["PageName"].ToString() : null,
                                ProfileImagePath = (!string.IsNullOrEmpty(rdr["ProfileImagePath"].ToString())) ? rdr["ProfileImagePath"].ToString() : null,
                                LikeCount = (!string.IsNullOrEmpty(rdr["LikesCount"].ToString())) ? Convert.ToInt64(rdr["LikesCount"].ToString()) : 0,
                                SubscribeCount = (!string.IsNullOrEmpty(rdr["Subscribers"].ToString())) ? Convert.ToInt64(rdr["Subscribers"].ToString()) : 0,
                                PageUUID = (!string.IsNullOrEmpty(rdr["PageUUID"].ToString())) ? rdr["PageUUID"].ToString() : null,
                                PageDescription = (!string.IsNullOrEmpty(rdr["PageDescription"].ToString())) ? (rdr["PageDescription"].ToString().Length > 47) ? rdr["PageDescription"].ToString().Substring(0,47) + "...".ToString() : rdr["PageDescription"].ToString() : "No Description available"
                            });
                        }
                        await con.CloseAsync();
                    }
                }
                if(UserPages.Count != 0)
                {
                    return UserPages;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return null;
            }
        }
        #endregion

        #region PageDetails
        public async Task<PageViewModel> PageDetails(string PageId, string UserId)
        {
            try
            {
                PageViewModel Page = new PageViewModel();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "SELECT p.PageName, p.PageDescription,p.ProfileImagePath,p.Subscribers,p.LikesCount, p.CreatedDate,p.ModifiedDate,p.PageUUID,\r\n\tp.IsActived,p.IsBlocked,pc.Privacytype, p.PageType\r\n\tFROM pages p \r\n    inner join privacycategory pc on pc.Id = p.PrivacyType\r\n    where PageId = (select Id from Pages where PageUUID = PageId) and OwnerId = (select Id from usertbl where UserGuid = UserId);";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@", PageId));
                        cmd.Parameters.Add(new MySqlParameter("@", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if(await rdr.ReadAsync())
                        {
                            Page.PageName = rdr[""].ToString();
                            Page.PageDescription= rdr[""].ToString();
                            Page.CategoryType= rdr[""].ToString();
                            Page.PageUUID = rdr[""].ToString();
                            Page.Subscribers = int.Parse( rdr[""].ToString());
                            Page.LikesCount = int.Parse( rdr[""].ToString());
                            Page.CreatedDate = DateTime.Parse( rdr[""].ToString());
                            Page.ModifiedDate = DateTime.Parse( rdr[""].ToString());
                            Page.PageUUID = rdr[""].ToString();
                        }
                    }
                    if(Page != null)
                    {
                        query = "";
                        using(MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add(new MySqlParameter("@", PageId));
                            cmd.Parameters.Add(new MySqlParameter("@", UserId));
                            DbDataReader rdr = await cmd.ExecuteReaderAsync();
                            while(await rdr.ReadAsync())
                            {
                                Page.Posts.Add(new PostsViewModel
                                {
                                    PostTitle = rdr["Title"].ToString(),
                                    PostCategoryName = rdr["Category"].ToString(),
                                    CreatedDate = DateTime.Parse(rdr[""].ToString()),
                                    ModifiedDate = DateTime.Parse(rdr[""].ToString()),
                                    FilePath = rdr["Title"].ToString(),
                                    MediaVisibilityState = rdr["Title"].ToString(),
                                    PostUUID = rdr["Category"].ToString()
                                });
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                return Page;
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return null;
            }
        }
        #endregion

        #region Page Users
        public async Task<List<UserInfoModel>> PageUsers(string PageId)
        {
            try
            {
                List<UserInfoModel> users = new List<UserInfoModel>();
                string ConnectionnString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string Query = "select ut.UserName, ut.FirstName, ut.LastName from usertbl ut" +
                    " inner join pagerole p on p.UserId = ut.Id" +
                    " where PageId = (select Id from pages where PageUUID = @PageId)";
                using(MySqlConnection con = new MySqlConnection(ConnectionnString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(Query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PageId", PageId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            users.Add(new UserInfoModel
                            {
                                UserName = rdr["UserName"].ToString(),
                                FirstName = rdr["FirstName"].ToString(),
                                LastName = rdr["LastName"].ToString(),
                                
                            });
                        }
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Check Name Validity
        public async Task<bool> IsValid(string pageName)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "select EXISTS (select id from pages where pagename = '@pagename')";

                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@pagename", pageName));
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

        #region Post

        #region CreatePage
        public async Task<string> CreatePage(PageModel page)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "INSERT INTO pages(OwnerId, PageName, PageDescription, ProfileImagePath, Subscribers, LikesCount, CreatedDate, ModifiedDate, PageUUID, IsActived, IsBlocked, PrivacyType, Pagetype) VALUES (@OwnerId, @PageName, @PageDescription, @ProfileImagePath, @Subscribers, @LikesCount,@CreatedDate, @ModifiedDate, @PageUUID, @IsActived, @IsBlocked, @PrivacyType, @Pagetype)";

                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand commmand = new MySqlCommand(query, con))
                    {
                        commmand.CommandType = CommandType.Text;
                        UUID = Guid.NewGuid().ToString();
                        commmand.Parameters.AddWithValue("@OwnerId", page.Userserialid);
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
                        commmand.Parameters.AddWithValue("@PrivacyType", page.Privacyserialid);
                        commmand.Parameters.AddWithValue("@Pagetype", page.Cateoryserialid);

                        if (await commmand.ExecuteNonQueryAsync() > 0)
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
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return null;
            }
        }
        #endregion

        #endregion

        #region Put
        #region Update Modifier For Page
        public async Task<bool> UpdateModifierForPage(PageUserModel pageUser)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "insert into pagerole(UserId, PageId, RoleId)" +
                    " values" +
                    " ((select Id from Usertbl where UserName = '' OR Email = ''), (select Id from pages where PageUUID = @PageId) , (select * from roleslist where RoleId = ''))";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PageId", pageUser.PageId));
                        cmd.Parameters.Add(new MySqlParameter("@UserId", pageUser.UserId));
                        cmd.Parameters.Add(new MySqlParameter("@RoleId", pageUser.RoleId));
                        if (Convert.ToInt32(cmd.ExecuteNonQueryAsync()) > 0)
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
                throw;
            }
        }
        #endregion
        #endregion
        public async Task<string> ModifyPage(PageModifyModel page)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "UPDATE pages SET PageName=@PageName, PageDescription=@PageDescription, ProfileImagePath=@ProfileImagePath, ModifiedDate=@ModifiedDate, IsActivated=@IsActivated where PageUUID=@PageId";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PageName", page.PageName);
                        cmd.Parameters.AddWithValue("@PageDescription", page.PageDescription);
                        cmd.Parameters.AddWithValue("@ProfileImagePath", page.ProfileImagePath);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTimeOffset.UtcNow);
                        cmd.Parameters.AddWithValue("@IsActivated", page.IsActivated);
                        cmd.Parameters.Add(new MySqlParameter("@PageId", page.PageUUID));

                        if (Convert.ToInt32(cmd.ExecuteNonQueryAsync()) > 0)
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

        #region UploadLogo
        public async Task<bool> UploadLogo(PageLogoModel pageLogo)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "UPDATE pages SET ProfileImagePath=@ProfileImagePath where PageUUID=@PageId";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ProfileImagePath", pageLogo.ImagePath);
                        cmd.Parameters.Add(new MySqlParameter("@PageId", pageLogo.PageGuid));

                        if (Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
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

        #region Update Page User
        public async Task<bool> UpdatePageUser(PageUserModel pageUser)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                
                string query = "INSERT INTO pagerole (UserId, PageId, RoleId)" +
                    " VALUES" +
                    " ((SELECT Id FROM usertbl where UserName = @UserGuid OR Email = @UserGuid)," +
                    " (SELECT Id FROM pages where PageUUID = @PageUUID)," +
                    " (SELECT Id FROM roleslist where RoleId = @RoleId))";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PageUUID", pageUser.PageId));
                        cmd.Parameters.Add(new MySqlParameter("@UserGuid", pageUser.UserId));
                        cmd.Parameters.Add(new MySqlParameter("@RoleId", pageUser.RoleId));
                        if(Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
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

        #region Delete
        #region DeletePage
        public async Task<bool> DeletePage(string UserId, string PageId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
                        cmd.Parameters.Add(new MySqlParameter("@pageid", PageId));
                        if (await cmd.ExecuteNonQueryAsync() != 0)
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
        #endregion
    }
}
