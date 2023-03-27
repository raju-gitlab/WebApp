using System.Collections.Generic;
using System.Web.Mvc;

namespace WP.Model.Models
{
    public class CreatePostModel : CommonModel
    {
        public string PostTitle { get; set; }
        [AllowHtml]
        public string PostDescription { get; set; }
        public int PostCategory { get; set; }
        public int MediaVisibility { get; set; }
        public string MediaVisibilityState { get; set; }
        public string PostUUID { get; set; }
        public string UserUUID { get; set; }
        public string PostTags { get; set; }
        public string PageUUID { get; set; }
    }
    public class PostsViewModel : CreatePostModel
    {
        public int UserId { get; set; }
        public string FilePath { get; set; }
        public long LikeCount { get; set; }
        public long DislikeCount { get; set; }
        public long SpamReportCount { get; set; }
        public bool IsBlocked { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostCategoryName { get; set; }
    }
    public class PageDetailsModel
    {
        public PageModel PageDetails { get; set;}
        public List<PostsViewModel> Posts { get; set; }
    }
}