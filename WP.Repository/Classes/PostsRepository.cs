using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Threading.Tasks;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Utillities.Utilities;

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
                string query = "SELECT u.FirstName, u.LastName, u.UserGuid, p.PostTitle, p.PostDescription, pg.CategoryName, p.FilePath, p.CreatedOn, P.likeCount, p.DislikeCount, p.PostUUID from Posts p" +
                    " INNER JOIN categories pg ON pg.Id = p.PostCategory" +
                    " INNER JOIN usertbl u ON u.Id = p.UserId" +
                    " WHERE P.IsBlocked = 0 AND p.MediaVisibility = 1" +
                    " UNION ALL" +
                    " SELECT u.FirstName, u.LastName, u.UserGuid, p.PostTitle, p.PostDescription, pg.CategoryName, p.FilePath, p.CreatedOn, P.likeCount, p.DislikeCount, p.PostUUID from Page_specific_posts p" +
                    " INNER JOIN categories pg ON pg.Id = p.PostCategory" +
                    " INNER JOIN usertbl u ON u.Id = p.UserId" +
                    " WHERE P.IsBlocked = 0 AND p.MediaVisibility = 1" +
                    " ORDER BY CreatedOn ASC";
                List<PostsViewModel> result = new List<PostsViewModel>();
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            result.Add(new PostsViewModel
                            {
                                FirstName = (rdr["FirstName"].ToString() != null) ? rdr["FirstName"].ToString() : "No Name",
                                LastName = (rdr["LastName"].ToString() != null) ? rdr["LastName"].ToString() : "No Name",
                                UserUUID = (rdr["UserGuid"].ToString() != null) ? rdr["UserGuid"].ToString() : "No Name",
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedOn"].ToString() != null) ? DateTime.Parse(rdr["CreatedOn"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                PostCategoryName = (rdr["CategoryName"].ToString() != null) ? rdr["CategoryName"].ToString() : null,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null
                            });
                        }

                        cmd.Dispose();
                        await con.CloseAsync();
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
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
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
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
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
        public async Task<PageDetailsModel> UserPosts(string UserId, string PageId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "SELECT p.PostTitle, p.PostDescription, p.FilePath, p.CreatedOn, p.ModifiedOn, p.LikeCount, p.DislikeCount,p.SpamReportCount, p.IsDeleted," +
                    " p.IsBlocked, p.PostUUID, c.CategoryName, pc.PrivacyType" +
                    " FROM page_specific_posts p" +
                    " left join Categories c on c.Id = p.PostCategory" +
                    " left join Privacycategory pc on pc.Id = p.MediaVisibility" +
                    " where p.PageId = (select Id from Pages where PageUUID = @pageId) and p.UserId = (select Id from usertbl where UserGuid = @UserId)";
                PageDetailsModel result = new PageDetailsModel();
                result.PageDetails = new PageModel();
                result.Posts = new List<PostsViewModel>();
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@pageId", PageId));
                        cmd.Parameters.Add(new MySqlParameter("@UserId", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (await rdr.ReadAsync())
                        {
                            result.Posts.Add(new PostsViewModel
                            {
                                PostTitle = (rdr["PostTitle"].ToString() != null) ? rdr["PostTitle"].ToString() : "No Title",
                                PostDescription = (rdr["PostDescription"].ToString() != null) ? rdr["PostDescription"].ToString() : null,
                                FilePath = (rdr["FilePath"].ToString() != null) ? rdr["FilePath"].ToString() : null,
                                CreatedDate = (rdr["CreatedOn"].ToString() != null) ? DateTime.Parse(rdr["CreatedOn"].ToString()) : DateTime.Parse(null),
                                ModifiedDate = (rdr["ModifiedOn"].ToString() != null) ? DateTime.Parse(rdr["ModifiedOn"].ToString()) : DateTime.Parse(null),
                                DislikeCount = (Convert.ToInt64(rdr["DislikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["DislikeCount"].ToString()) : 0,
                                LikeCount = (Convert.ToInt64(rdr["LikeCount"].ToString()) != -1) ? Convert.ToInt64(rdr["LikeCount"].ToString()) : 0,
                                IsBlocked = Convert.ToBoolean(rdr["IsBlocked"]),
                                PostCategoryName = (rdr["CategoryName"].ToString() != null) ? rdr["CategoryName"].ToString() : null,
                                SpamReportCount = (Convert.ToInt64(rdr["SpamReportCount"].ToString()) != -1) ? Convert.ToInt64(rdr["SpamReportCount"].ToString()) : 0,
                                PostUUID = (rdr["PostUUID"].ToString() != null) ? rdr["PostUUID"].ToString() : null,
                                MediaVisibilityState = (rdr["Privacytype"].ToString() != null) ? rdr["Privacytype"].ToString() : null
                            });
                        }
                        await con.CloseAsync();
                    }
                    await con.OpenAsync();
                    query = "SELECT p.PageName, p.PageDescription, p.ProfileImagePath, p.PageUUID, c.CategoryName, p.CreatedDate,p.IsActived, p.LikesCount,p.Subscribers, pc.PrivacyType, p.IsBlocked" +
                   " FROM pages p" +
                   " inner join categories c on c.Id = p.PageType" +
                   " inner join privacycategory pc on pc.Id = p.Privacytype" +
                   " where p.PageUUID = @pageId";
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PageId", PageId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        if (await rdr.ReadAsync())
                        {
                            result.PageDetails.PageName = (!string.IsNullOrEmpty(rdr["PageName"].ToString()) ? rdr["PageName"].ToString() : null);
                            result.PageDetails.PageDescription = (!string.IsNullOrEmpty(rdr["PageDescription"].ToString()) ? rdr["PageDescription"].ToString() : null);
                            result.PageDetails.ProfileImagePath = (!string.IsNullOrEmpty(rdr["ProfileImagePath"].ToString()) ? rdr["ProfileImagePath"].ToString() : null);
                            result.PageDetails.PageUUID = (!string.IsNullOrEmpty(rdr["PageUUID"].ToString()) ? rdr["PageUUID"].ToString() : null);
                            result.PageDetails.CategoryType = (!string.IsNullOrEmpty(rdr["CategoryName"].ToString()) ? rdr["CategoryName"].ToString() : null);
                            result.PageDetails.CreatedDate = (!string.IsNullOrEmpty(rdr["CreatedDate"].ToString()) ? DateTime.Parse(rdr["CreatedDate"].ToString()) : DateTime.MinValue);
                            result.PageDetails.IsActivated = (!string.IsNullOrEmpty(rdr["IsActived"].ToString()) ? Convert.ToBoolean(rdr["IsActived"].ToString()) : false);
                            result.PageDetails.LikesCount = (!string.IsNullOrEmpty(rdr["LikesCount"].ToString()) ? Convert.ToInt64(rdr["LikesCount"].ToString()) : 0);
                            result.PageDetails.Subscribers = (!string.IsNullOrEmpty(rdr["Subscribers"].ToString()) ? Convert.ToInt64(rdr["Subscribers"].ToString()) : 0);
                            result.PageDetails.PrivacyType = (!string.IsNullOrEmpty(rdr["PrivacyType"].ToString()) ? Convert.ToString(rdr["PrivacyType"].ToString()) : null);
                            result.PageDetails.IsBlocked = (!string.IsNullOrEmpty(rdr["IsBlocked"].ToString()) ? Convert.ToBoolean(rdr["IsBlocked"].ToString()) : true);
                        }
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
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
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
                foreach (string str in tags)
                {
                    query += $"('{str}'),";
                }
                query = query.Remove(query.Length - 1);
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
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
                throw;
            }
        }
        #endregion  

        #region UpdateTagslist
        public async Task<bool> UpdateTagslist(string[] tags, string PostId, string PageId)
        {
            try
            {
                string ConnectinoString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                IEnumerable<string> uniqueItems = tags.Distinct<string>();
                string query = "INSERT INTO page_post_tags(PageId,PostId, TagId) values ((select Id from Pages where PageUUID = @PageId),(select Id from page_specific_posts where PostUUID = @PostId), (select Id from Tags Where tagename = @TagName))";
                using (MySqlConnection con = new MySqlConnection(ConnectinoString))
                {
                    await con.OpenAsync();
                    foreach(string item in uniqueItems)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.Add(new MySqlParameter("@PageId", PageId));
                            cmd.Parameters.Add(new MySqlParameter("@PostId", PostId));
                            cmd.Parameters.Add(new MySqlParameter("@TagName", item));
                            cmd.CommandType = CommandType.Text;
                            if (Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region UpdatePostsTagslist
        public async Task<bool> UpdatePostsTagslist(string[] tags, string PostId)
        {
            try
            {
                string ConnectinoString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                IEnumerable<string> uniqueItems = tags.Distinct<string>();
                string query = "INSERT INTO post_tags(PostId, TagId) values ((select Id from posts where PostUUID = @PostId), (select Id from Tags Where tagename = @TagName))";
                using (MySqlConnection con = new MySqlConnection(ConnectinoString))
                {
                    await con.OpenAsync();
                    foreach (string item in uniqueItems)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.Add(new MySqlParameter("@PostId", PostId));
                            cmd.Parameters.Add(new MySqlParameter("@TagName", item));
                            cmd.CommandType = CommandType.Text;
                            if (Convert.ToInt32(await cmd.ExecuteNonQueryAsync()) > 0)
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #endregion
        #endregion

        #region Top Posts By User
        public async Task<List<CreatePostModel>> GetTopPostsByUserId(string UserId)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                List<CreatePostModel> posts = new List<CreatePostModel>();
                string query = "select PostTitle, PostDescription, FilePath, CreatedOn, LikeCount from page_specific_posts" +
                    " union all" +
                    " select PostTitle, PostDescription, FilePath, CreatedOn, LikeCount from posts" +
                    " where UserId = (select Id from usertbl where UserGuid = @userid) order by LikeCount desc LIMIT 5";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@userid", UserId));
                        DbDataReader rdr = await cmd.ExecuteReaderAsync();
                        while (rdr.Read()) 
                        {
                            posts.Add(new CreatePostModel()
                            {
                                PostTitle = !string.IsNullOrEmpty(rdr["PostTitle"].ToString()) ? rdr["PostTitle"].ToString() : string.Empty,
                                PostDescription = !string.IsNullOrEmpty(rdr["PostDescription"].ToString()) ? rdr["PostDescription"].ToString() : string.Empty,
                                FilePath = !string.IsNullOrEmpty(rdr["FilePath"].ToString()) ? rdr["FilePath"].ToString() : string.Empty,
                                CreatedDate = !string.IsNullOrEmpty(rdr["CreatedOn"].ToString()) ? DateTime.Parse(rdr["CreatedOn"].ToString()) : DateTime.Parse("")
                            });
                        }
                    }
                    await con.CloseAsync();
                }
                return posts;
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

        #region Update Existing Post
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

        #region Upload Post Image
        public async Task<bool> UploadPostImage(CreatePostModel updatePost)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "update posts set FilePath = @FilePath where PostUUID = @PostId";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PostId", updatePost.PostUUID));
                        cmd.Parameters.AddWithValue("@FilePath", updatePost.FilePath);

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
                throw ex;
            }
        }
        #endregion

        #region Upload Post Image
        public async Task<bool> UploadPagePostImage(CreatePostModel updatePost)
        {
            try
            {
                string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                string query = "update page_specific_posts set FilePath = @FilePath where PostUUID = @PostId";
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("@PostId", updatePost.PostUUID));
                        cmd.Parameters.AddWithValue("@FilePath", updatePost.FilePath);

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
                throw ex;
            }
        }
        #endregion
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
                using (MySqlConnection con = new MySqlConnection(ConnectionString))
                {
                    await con.OpenAsync();
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add(new MySqlParameter("", pageId));
                        cmd.Parameters.Add(new MySqlParameter("", PostId));

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
                throw;
            }
        }


        #endregion
        #endregion
    }
}