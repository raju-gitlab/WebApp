using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Model.Models
{
    public class PostsModel
    {
        public int Id { get; set; }
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public int UserId { get; set; }
        public int PostCategory { get; set; }
        public int MediaVisibility { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public long LikeCount { get; set; }
        public long DislikeCount { get; set; }
        public long SpamReportCount { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }
        public string postUUID { get; set; }
    }
}
