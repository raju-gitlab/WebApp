using System;
using System.Collections.Generic;
using WP.Model.Utilities;

namespace WP.Model.Models
{
    public class PageModel : ListIds
    {
        public string OwnerId { get; set; }
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public string ProfileImagePath { get; set; } = string.Empty;
        public long Subscribers { get; set; }
        public long LikesCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PageUUID { get; set; }
        public bool IsActivated { get; set; }
        public bool IsBlocked { get; set; }
        public string PrivacyType { get; set; }
        public string CategoryType { get; set; }
    }
    public class PageModifyModel
    {
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public string ProfileImagePath { get; set; }
        public bool IsActivated { get; set; }
        public string PageUUID { get; set; }
    }
    public class PageCardModel : PageModifyModel
    {
        public string AdminName { get; set; }
        public string AdminUUID { get; set; }
        public long LikeCount { get; set; }
        public long SubscribeCount { get; set; }
    }
    public class PageViewModel : PageModel
    {
        public bool IsAdminUser { get; set; }
        public List<PostsViewModel> Posts { get; set; }
    }
    public class PageUserModel
    {
        public string UserId { get; set; }
        public string PageId { get; set; }
        public string RoleId { get; set; }
    }
    public class PageLogoModel
    {
        public string ImagePath { get; set; }
        public string PageGuid { get; set; }
    }
}