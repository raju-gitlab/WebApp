using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Business.Interfaces
{
    public interface IPostsBusiness
    {
        Task<List<PostsViewModel>> GetAllPosts();
        Task<List<PostsViewModel>> GetAllPostsByUserId(string UserId);
        Task<List<PostsViewModel>> GetAllPostsByPostCategory(string category);
        Task<List<PostsViewModel>> GetPostsByCustomeFindAttributes();
        Task<string> CreatePost(PostsViewModel posts);
        Task<string> CreatePagePost(PostsViewModel posts);
        Task<List<PostsViewModel>> UserPosts(string UserId, string PageId);
        Task<PostsViewModel> UpdateExistingPost(PostsViewModel post);
        Task<bool> DeletePost(string PostId, string UserId);
        Task<bool> DeletePagePost(string pageId, string PostId);
        Task<List<PostsViewModel>> TrendsPosts();
        Task<bool> UpdateTagslist(string[] tags, string PostId);
        Task<bool> AddTags(string[] tags);
    }
}
