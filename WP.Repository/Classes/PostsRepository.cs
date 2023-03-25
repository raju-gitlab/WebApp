using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Policy;
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
        public async Task<List<PostsViewModel>> GetAllPosts()
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "SELECT u.FirstName, u.LastName, u.UserGuid, p.PostTitle, p.PostDescription, pg.CategoryName, p.FilePath, p.CreatedOn, P.likeCount, p.DislikeCount, p.PostUUID from Posts p"+
                " INNER JOIN postcategories pg ON pg.Id = p.PostCategory " +
                " INNER JOIN usertbl u ON u.Id = p.UserId " +
                " WHERE P.IsBlocked = 0 AND p.MediaVisibility = 1 ORDER BY p.CreatedOn ASC";
                List<PostsViewModel> result = null;
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while(await rdr.ReadAsync())
                        {
                            result.Add(new PostsViewModel
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
        public async Task<List<PostsViewModel>> GetAllPostsByUserId(string UserId)
        {
            try
            {
                List<PostsViewModel> result = null;
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
                            result.Add(new PostsViewModel
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
        public async Task<List<PostsViewModel>> GetAllPostsByPostCategory(string category)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                List<PostsViewModel> result = null;
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
                            result.Add(new PostsViewModel
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
        public async Task<List<PostsViewModel>> UserPosts(string UserId, string PageId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                List<PostsViewModel> result = null;
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
                            result.Add(new PostsViewModel
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
        public async Task<List<PostsViewModel>> GetPostsByCustomeFindAttributes()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Trend Posts
        public async Task<List<PostsViewModel>> TrendsPosts()
        {
            try
            {
                List<PostsViewModel> Posts = new List<PostsViewModel>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "";
                using(MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            Posts.Add(new PostsViewModel
                            {
                                FilePath = !string.IsNullOrEmpty(rdr["FilePath"].ToString()) ? rdr["FilePath"].ToString() : string.Empty,
                                PostCategoryName = !string.IsNullOrEmpty(rdr["PostCategoryName"].ToString()) ? rdr["PostCategoryName"].ToString() : string.Empty,
                                PostTags = !string.IsNullOrEmpty(rdr["PostTags"].ToString()) ? rdr["PostTags"].ToString() : string.Empty,
                                PostTitle = !string.IsNullOrEmpty(rdr["PostTitle"].ToString()) ? rdr["PostTitle"].ToString() : string.Empty,
                                PostUUID = !string.IsNullOrEmpty(rdr["PostUUID"].ToString()) ? rdr["PostUUID"].ToString() : string.Empty,
                                FirstName = !string.IsNullOrEmpty(rdr["FirstName"].ToString()) ? rdr["FirstName"].ToString() : string.Empty,
                                UserUUID = !string.IsNullOrEmpty(rdr["UserUUID"].ToString()) ? rdr["UserUUID"].ToString() : string.Empty
                            });
                        }
                    }
                    await con.CloseAsync();
                }
                return Posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Post
        #region CreatePost
        public async Task<string> CreatePost(PostsViewModel posts)
        {
            try
            {
                UUID = Guid.NewGuid().ToString();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "insert into posts (PostTitle, PostDescription, UserId, PostCategory, MediaVisibility, FilePath, CreatedOn, ModifiedOn, LikeCount, DislikeCount, SpamReportCount, IsBlocked, PostUUID)" +
                                " VALUES(@PostTitle, @PostDescription, @UserId, @PostCategory, @MediaVisibility, @FilePath, @CreatedOn, @ModifiedOn, @LikeCount, @DislikeCount, @SpamReportCount, @IsBlocked, @PostUUID)";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PostTitle", posts.PostTitle);
                        cmd.Parameters.AddWithValue("@PostDescription", posts.PostDescription);
                        cmd.Parameters.AddWithValue("@PostCategory", posts.Cateoryserialid);
                        cmd.Parameters.AddWithValue("@UserId", posts.Userserialid);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@ModifiedOn", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@LikeCount", 0);
                        cmd.Parameters.AddWithValue("@DislikeCount", posts.DislikeCount);
                        cmd.Parameters.AddWithValue("@MediaVisibility", 1);
                        cmd.Parameters.AddWithValue("@PostUUID", UUID);
                        cmd.Parameters.AddWithValue("@SpamReportCount", 0);
                        cmd.Parameters.AddWithValue("@IsBlocked", false);
                        cmd.Parameters.AddWithValue("@FilePath", posts.FilePath);
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
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region Create page post
        public async Task<string> CreatePagePost(PostsViewModel posts)
        {
            try
            {
                UUID = Guid.NewGuid().ToString();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "INSERT INTO page_specific_posts " +
                " ( PostTitle, PostDescription, UserId, PageId, PostCategory, MediaVisibility, FilePath, CreatedOn,ModifiedOn,LikeCount," +
                " DislikeCount, SpamReportCount, IsDeleted, IsBlocked, PostUUID)" +
                " VALUES" +
                " (@PostTitle, @PostDescription, @userid, @pageid, @categoryid, @privacyid, @FilePath, @CreatedOn,@ModifiedOn,@LikeCount," +
                " @DislikeCount, @SpamReportCount, @IsDeleted, @IsBlocked, @postUUID)";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@userid", posts.Userserialid);
                        cmd.Parameters.AddWithValue("@pageId", posts.PageserialId);
                        cmd.Parameters.AddWithValue("@categoryId", posts.Cateoryserialid);
                        cmd.Parameters.AddWithValue("@privacyId", posts.Privacyserialid);
                        cmd.Parameters.AddWithValue("@PostTitle", posts.PostTitle);
                        cmd.Parameters.AddWithValue("@PostDescription", posts.PostDescription);
                        cmd.Parameters.AddWithValue("@LikeCount", posts.LikeCount);
                        cmd.Parameters.AddWithValue("@DislikeCount", posts.DislikeCount);
                        cmd.Parameters.AddWithValue("@SpamReportCount", posts.SpamReportCount);
                        cmd.Parameters.AddWithValue("@IsDeleted", false);
                        cmd.Parameters.AddWithValue("@IsBlocked", false);
                        cmd.Parameters.AddWithValue("@FilePath", (string.IsNullOrEmpty(posts.FilePath)) ? "emprt" : posts.FilePath);
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@ModifiedOn", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@postUUID", UUID);

                        if (await cmd.ExecuteNonQueryAsync() > 0)
                        {
                            await con.CloseAsync();
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
                throw;
            }
        }
        #endregion

        #region Tags
        #region Post

        #region AddTags
        public async Task<bool> AddTags(string[] tags)
        {
            try
            {
                IEnumerable<string> uniqueItems = tags.Distinct<string>();
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "insert into tags (tagename) values  ";
                foreach(string str in tags)
                {
                    query += $"('{str}'),";
                }
                query = query.Remove(query.Length - 1);
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using(MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
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
                throw;
            }
        }
        #endregion  

        #region UpdateTagslist
        public async Task<bool> UpdateTagslist(string[] tags, string PostId)
        {
            try
            {
                string ConnectinoString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                IEnumerable<string> uniqueItems = tags.Distinct<string>();
                string query = "INSERT INTO Post_tags(PostId, PagePostId, TagId) values ";
                foreach (string item in uniqueItems)
                {
                    query += $"((select Id from Posts where PostUUID = '{PostId}'), '4', (select Id from Tags Where tagename = '{item}'))," ;
                }
                query = query.Remove(query.Length - 1);
                using (MySqlConnection con = new MySqlConnection(ConnectinoString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
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
                throw;
            }
        }
        #endregion
        #endregion
        #endregion
        #endregion

        #region PUT
        public async Task<PostsViewModel> UpdateExistingPost(CreatePostModel post)
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