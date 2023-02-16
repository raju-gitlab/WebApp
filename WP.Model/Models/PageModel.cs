﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WP.Model.Models
{
    public class PageModel
    {
        [Required]
        public string OwnerId { get; set; }
        [Required]
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        //public string ProfileImagePath { get; set; }
        public long Subscribers { get; set; }
        public long LikesCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string PageUUID { get; set; }
        public bool IsActivated { get; set; }
        public bool IsBlocked { get; set; }
        public string PrivacyType { get; set; }
        public string CategoryType { get; set; }
        public HttpPostedFileBase ProfileImagePath { get; set; }

    }
    
    public class PageModifyModel
    {
        public string PageName { get; set; }
        public string PageDescription { get; set; }
        public string ProfileImagePath { get; set; }
        public bool IsActivated { get; set; }
        public string PageUUID { get; set; }
    }

}
