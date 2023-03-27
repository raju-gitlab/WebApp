using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Repository.Interfaces.Misc;
using WP.Utillities.Utilities;

namespace WP.Business.Classes
{
    public class PostsBusiness : IPostsBusiness
    {
        #region Consts
        private readonly IPostsRepository _postsRepository;
        private readonly IMiscRepository _miscRepository;
        public PostsBusiness(IPostsRepository postsRepository, IMiscRepository miscRepository)
        {
            this._postsRepository = postsRepository;
            this._miscRepository = miscRepository;
        }
        #endregion

        #region Get
        #region Trends Posts
        public async Task<List<PostsViewModel>> TrendsPosts()
        {
            try
            {
                return await this._postsRepository.TrendsPosts();
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return null;
            }
        }
        #endregion
        #endregion

        #region Post
        #region Create post
        public async Task<string> CreatePost(PostsViewModel posts)
        {
            try
            {
                posts.Userserialid = await this._miscRepository.GetUserId(posts.UserUUID);
                posts.Cateoryserialid = await this._miscRepository.GetCategoryId(posts.MediaVisibilityState);
                string result = await this._postsRepository.CreatePost(posts);
                if (!string.IsNullOrEmpty(result))
                {
                    if(posts.UniqueTags.Length > 0)
                    {
                        string[] PostTagslist = posts.UniqueTags.Split(',');
                        string str = "";
                        if (await this._postsRepository.AddTags(PostTagslist))
                        {
                            string[] tags = posts.PostTags.Split(',');
                            tags.Union(PostTagslist);
                            var str1 = "";
                            if (await this._postsRepository.UpdateTagslist(tags, result, 1))
                            {
                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        string[] tags = posts.PostTags.Split(',');
                        tags.Union(posts.UniqueTags.Split(','));
                        var str1 = "";
                        if (await this._postsRepository.UpdateTagslist(tags, result, 1))
                        {
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
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

        #region Create Page Post
        public async Task<string> CreatePagePost(PostsViewModel posts)
        {
            posts.Userserialid = await this._miscRepository.GetUserId(posts.UserUUID);
            posts.Cateoryserialid = await this._miscRepository.GetCategoryId(posts.PostCategoryName);
            posts.Privacyserialid = await this._miscRepository.GetPrivacyId(posts.MediaVisibilityState);
            posts.PageserialId = await this._miscRepository.GetPageId(posts.PageUUID);
            if(posts.Userserialid == -1)
            {
                return string.Empty;
            }
            else if (posts.Cateoryserialid == -1)
            {
                return string.Empty;
            }
            else if (posts.Privacyserialid == -1)
            {
                return string.Empty;
            }
            else if(posts.PageserialId == -1)
            {
                return string.Empty;
            }
            else
            {
                string result = await this._postsRepository.CreatePagePost(posts);
                if (!string.IsNullOrEmpty(result))
                {
                    if (posts.UniqueTags.Length > 0)
                    {
                        string[] PostTagslist = posts.UniqueTags.Split(',');
                        if (await this._postsRepository.AddTags(PostTagslist))
                        {
                            string[] tags = posts.PostTags.Split(',');
                            tags.Union(PostTagslist);
                            if (await this._postsRepository.UpdateTagslist(tags, result, 2))
                            {
                                return result;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        string[] tags = posts.PostTags.Split(',');
                        tags.Union(posts.UniqueTags.Split(','));
                        var str1 = "";
                        if (await this._postsRepository.UpdateTagslist(tags, result, 1))
                        {
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Tags
        #region Post
        #region AddTags
        public async Task<bool> AddTags(string[] tags)
        {
            if(tags.Length == 0)
            {
                return true;
            }
            else
            {
                if(await this._postsRepository.AddTags(tags))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion  
        #endregion
        #endregion

        public Task<bool> DeletePagePost(string pageId, string PostId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string PostId, string UserId)
        {
            throw new NotImplementedException();
        }

        #region Get all post
        public async Task<List<PostsViewModel>> GetAllPosts()
        {
            try
            {
                return await this._postsRepository.GetAllPosts();
            }
            catch (Exception ex)
            {

                throw;
            }
        } 
        #endregion

        public Task<List<PostsViewModel>> GetAllPostsByPostCategory()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsViewModel>> GetAllPostsByPostCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsViewModel>> GetAllPostsByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsViewModel>> GetPostsByCustomeFindAttributes()
        {
            throw new NotImplementedException();
        }

        public Task<PostsViewModel> UpdateExistingPost(PostsViewModel post)
        {
            throw new NotImplementedException();
        }

        public async Task<PageDetailsModel> UserPosts(string UserId, string PageId)
        {
            try
            {
                if(string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(PageId))
                {
                    return null;
                }
                else
                {
                    return await this._postsRepository.UserPosts(UserId, PageId);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region UpdateTagslist
        public async Task<bool> UpdateTagslist(string[] tags, string PostId, int PostType)
        {
            try
            {
                if(tags.Length == 0)
                {
                    return true;
                }
                else
                {
                    return await this._postsRepository.UpdateTagslist(tags, PostId, PostType);
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
