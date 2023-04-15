using System.Collections.Generic;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IPostsRepository
    {
        Task<List<PostsViewModel>> GetAllPosts();
        Task<List<CreatePostModel>> GetTopPostsByUserId(string UserId);
        Task<List<PostsViewModel>> GetAllPostsByUserId(string UserId);
        Task<List<PostsViewModel>> GetAllPostsByPostCategory(string category);
        Task<List<PostsViewModel>> GetPostsByCustomeFindAttributes();
        Task<string> CreatePost(PostsViewModel posts);
        Task<string> CreatePagePost(PostsViewModel posts);
        Task<PageDetailsModel> UserPosts(string UserId, string PageId);
        Task<PostsViewModel> UpdateExistingPost(CreatePostModel post);
        Task<bool> DeletePost(string PostId, string UserId);
        Task<bool> DeletePagePost(string pageId, string PostId);
        Task<List<PostsViewModel>> TrendsPosts();
        Task<bool> UpdateTagslist(string[] tags, string PostId, string PageId);
        Task<bool> UpdatePostsTagslist(string[] tags, string PostId);
        Task<bool> AddTags(string[] tags);
        Task<bool> UploadPostImage(CreatePostModel updatePost);
        Task<bool> UploadPagePostImage(CreatePostModel updatePost);
    }
}