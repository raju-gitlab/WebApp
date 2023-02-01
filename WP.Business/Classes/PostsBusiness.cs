using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Utillities.Exceptions;

namespace WP.Business.Classes
{
    public class PostsBusiness : IPostsBusiness
    {
        #region Consts
        private readonly IPostsRepository _postsRepository;
        public PostsBusiness(IPostsRepository postsRepository)
        {
            this._postsRepository = postsRepository;
        }
        #endregion

        #region Post
        public async Task<string> CreatePost(PostsModel posts)
        {
            try
            {
                string result = await this._postsRepository.CreatePost(posts);
                if(string.IsNullOrEmpty(result))
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

        public Task<bool> DeletePagePost(string pageId, string PostId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(string PostId, string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> GetAllPostsByPostCategory()
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> GetAllPostsByPostCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> GetAllPostsByUserId(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> GetPostsByCustomeFindAttributes()
        {
            throw new NotImplementedException();
        }

        public Task<PostsModel> UpdateExistingPost(PostsModel post)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostsModel>> UserPosts(string UserId, string PageId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
