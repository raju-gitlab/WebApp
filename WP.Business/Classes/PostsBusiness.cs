using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Repository.Interfaces.Misc;
using WP.Utillities.Exceptions;

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

        #region Post
        #region Create post
        public async Task<string> CreatePost(PostsViewModel posts)
        {
            try
            {
                posts.Userserialid = await this._miscRepository.GetUserId(posts.UserUUID);
                posts.Cateoryserialid = await this._miscRepository.GetCategoryId(posts.PostUUID);
                posts.Privacyserialid = await this._miscRepository.GetPrivacyId(posts.MediaVisibilityState);
                string result = await this._postsRepository.CreatePost(posts);
                if (!string.IsNullOrEmpty(result))
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

        #region MyRegion
        public async Task<string> CreatePagePost(PostsViewModel posts)
        {
            posts.Userserialid = await this._miscRepository.GetUserId(posts.UserUUID);
            posts.Cateoryserialid = await this._miscRepository.GetCategoryId(posts.PostUUID);
            posts.Privacyserialid = await this._miscRepository.GetPrivacyId(posts.MediaVisibilityState);
            if(posts.Userserialid == -1)
            {
                return "";
            }
            else if (posts.Cateoryserialid == -1)
            {
                return "";
            }
            else if (posts.Privacyserialid == -1)
            {
                return "";
            }
            else
            {
                return await this._postsRepository.CreatePagePost(posts);
            }
        }

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

        public Task<List<PostsViewModel>> UserPosts(string UserId, string PageId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
