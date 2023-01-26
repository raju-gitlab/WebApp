using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Model.Models
{
    public class PageModel
    {
        public string OwnerId { get; set; } // Foreign Key Constraint
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public string ProfileImagePath { get; set; }
        public long Subscribers { get; set; }
        public long LikesCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime PageUUID { get; set; }
        public bool IsActivated { get; set; }
        public bool IsBlocked { get; set; }
    }
}
