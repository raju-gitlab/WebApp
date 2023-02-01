using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;

namespace WP.Repository.Classes
{
    public class PostsRepository : IPostsRepository
    {
        #region Params
        public string userid { get; set; }
        public string UUID { get; set; }
        #endregion

        #region GET
        #region GetAllPosts
        public async Task<List<PostsModel>> GetAllPosts()
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                List<PostsModel> result = null;
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
                        {
                            result.Add(new PostsModel
                            {
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedDate"].ToString() != null) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.Parse(null),
                                ModifiedDate = (rdr["ModifiedDate"].ToString() != null) ? DateTime.Parse(rdr["ModifiedDate"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                IsBlocked = Convert.ToBoolean(rdr["IsBlocked"]),
                                PostCategoryName = (rdr["PostCategoryName"].ToString() != null) ? rdr["PostCategoryName"].ToString() : null,
                                SpamReportCount = (Convert.ToInt64(rdr["SpamReportCount"].ToString()) != -1) ? Convert.ToInt64(rdr["SpamReportCount"].ToString()) : 0,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null
                            });
                        }

                        cmd.Dispose();
                        await con.CloseAsync();
                    }
                }

                if(result != null)
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
                throw;
            }
        }
        #endregion

        #region GetAllPostsByUserId
        public async Task<List<PostsModel>> GetAllPostsByUserId(string UserId)
        {
            try
            {
                List<PostsModel> result = null;
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            result.Add(new PostsModel
                            {
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedDate"].ToString() != null) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.Parse(null),
                                ModifiedDate = (rdr["ModifiedDate"].ToString() != null) ? DateTime.Parse(rdr["ModifiedDate"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                IsBlocked = Convert.ToBoolean(rdr["IsBlocked"]),
                                PostCategoryName = (rdr["PostCategoryName"].ToString() != null) ? rdr["PostCategoryName"].ToString() : null,
                                SpamReportCount = (Convert.ToInt64(rdr["SpamReportCount"].ToString()) != -1) ? Convert.ToInt64(rdr["SpamReportCount"].ToString()) : 0,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null
                            });
                        }

                        cmd.Dispose();
                        await con.CloseAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region GetAllPostsByPostCategory
        public async Task<List<PostsModel>> GetAllPostsByPostCategory(string category)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                List<PostsModel> result = null;
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("", category));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            result.Add(new PostsModel
                            {
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedDate"].ToString() != null) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.Parse(null),
                                ModifiedDate = (rdr["ModifiedDate"].ToString() != null) ? DateTime.Parse(rdr["ModifiedDate"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                IsBlocked = Convert.ToBoolean(rdr["IsBlocked"]),
                                PostCategoryName = (rdr["PostCategoryName"].ToString() != null) ? rdr["PostCategoryName"].ToString() : null,
                                SpamReportCount = (Convert.ToInt64(rdr["SpamReportCount"].ToString()) != -1) ? Convert.ToInt64(rdr["SpamReportCount"].ToString()) : 0,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null
                            });
                        }

                        cmd.Dispose();
                        await con.CloseAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region UserPosts
        public async Task<List<PostsModel>> UserPosts(string UserId, string PageId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                List<PostsModel> result = null;
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.Add(new MySqlParameter("", category));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            result.Add(new PostsModel
                            {
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedDate"].ToString() != null) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.Parse(null),
                                ModifiedDate = (rdr["ModifiedDate"].ToString() != null) ? DateTime.Parse(rdr["ModifiedDate"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                IsBlocked = Convert.ToBoolean(rdr["IsBlocked"]),
                                PostCategoryName = (rdr["PostCategoryName"].ToString() != null) ? rdr["PostCategoryName"].ToString() : null,
                                SpamReportCount = (Convert.ToInt64(rdr["SpamReportCount"].ToString()) != -1) ? Convert.ToInt64(rdr["SpamReportCount"].ToString()) : 0,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null,
                                MediaVisibilityState = (rdr["MediaVisibilityState"].ToString() != null) ? rdr["MediaVisibilityState"].ToString() : null
                            });
                        }
                        cmd.Dispose();
                        await con.CloseAsync();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region MyRegion
        public async Task<List<PostsModel>> GetPostsByCustomeFindAttributes()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Post
        #region CreateNewPost
        public async Task<string> CreatePost(PostsModel posts)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                MySqlCommand cmd = null;
                MySqlConnection con = null;
                using (con = new MySqlConnection(ConnectionString))
                {
                    query = "SELECT Id from  usertbl where UserGuid = @userId";
                    await con.OpenAsync();
                    using (cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("userId", posts.UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            userid = rdr["Id"].ToString();
                        }

                        await con.CloseAsync();
                        if (!string.IsNullOrEmpty(userid))
                        {
                            using (con = new MySqlConnection(ConnectionString))
                            {
                                query = "";
                                UUID = Guid.NewGuid().ToString();
                                await con.OpenAsync();
                                using (cmd = new MySqlCommand(query, con))
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.Parameters.AddWithValue("@PostTitle", posts.PostTitle);
                                    cmd.Parameters.AddWithValue("@PostDescription", posts.PostDescription);
                                    cmd.Parameters.AddWithValue("@PostCategory", posts.PostCategory);
                                    cmd.Parameters.AddWithValue("@UserId", userid);
                                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
                                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.UtcNow);
                                    cmd.Parameters.AddWithValue("@LikeCount", 0);
                                    cmd.Parameters.AddWithValue("@MediaVisibility", posts.MediaVisibility);
                                    cmd.Parameters.AddWithValue("@PostCategory", posts.PostCategory);
                                    cmd.Parameters.AddWithValue("@SpamReportCount", 0);
                                    cmd.Parameters.AddWithValue("@PostUUID", UUID);
                                    cmd.Parameters.AddWithValue("@", posts);
                                    cmd.Parameters.AddWithValue("@", posts);

                                    if (await cmd.ExecuteNonQueryAsync() > 0)
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
        #endregion


        #endregion

        #region PUT
        public async Task<PostsModel> UpdateExistingPost(CreatePostModel post)
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
                        cmd.Parameters.Add(new MySqlParameter("@postid", post.PostUUID));
                        cmd.Parameters.Add(new MySqlParameter("@userid", post.UserUUID));
                        cmd.Parameters.AddWithValue("@MediaVisibility", post.MediaVisibility);
                        cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@PostCategory", post.PostCategory);
                        cmd.Parameters.AddWithValue("@PostDescription", post.PostDescription);

                        if (await cmd.ExecuteNonQueryAsync() > 0)
                        {
                            return null;
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

                throw;
            }
        }
        #endregion

        #region DELETE

        #region DeletePost
        public async Task<bool> DeletePost(string PostId, string UserId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "Delete from Posts where PostUUID = @postid AND UserId = @userid";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@postid", PostId));
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
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

                throw;
            }
        }
        #endregion

        #region DeletePagePost
        // UserId need to be added
        public async Task<bool> DeletePagePost(string pageId, string PostId)
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
                        cmd.Parameters.Add(new MySqlParameter("", pageId));
                        cmd.Parameters.Add(new MySqlParameter("", PostId));

                        if(await cmd.ExecuteNonQueryAsync() > 0)
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
                throw;
            }
        }
        #endregion
        #endregion
    }
}
