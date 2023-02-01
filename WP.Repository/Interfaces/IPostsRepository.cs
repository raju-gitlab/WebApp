using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IPostsRepository
    {
        Task<List<PostsModel>> GetAllPosts();
        Task<List<PostsModel>> GetAllPostsByUserId(string UserId);
        Task<List<PostsModel>> GetAllPostsByPostCategory(string category);
        Task<List<PostsModel>> GetPostsByCustomeFindAttributes();
        Task<string> CreatePost(PostsModel posts);
        Task<List<PostsModel>> UserPosts(string UserId, string PageId);
        Task<PostsModel> UpdateExistingPost(CreatePostModel post);
        Task<bool> DeletePost(string PostId, string UserId);
        Task<bool> DeletePagePost(string pageId, string PostId);
    }
}
