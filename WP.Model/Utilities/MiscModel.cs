﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Model.Utilities
{
    public class MiscModel
    {
    }

    public class CategoryModel
    {
        public string CategoryName { get; set; }
        public string CategoryUUID { get; set; }
    }
    public class PrivacyModel
    {
        public string PrivacyType { get; set; }
        public string PrivacyUUID { get; set; }
    }
    public class ListIds
    {
        public int Userserialid { get; set; }
        public int Cateoryserialid { get; set; }
        public int Privacyserialid { get; set; }
        public int PageserialId { get; set; }
    }
    public class TagsModel
    {
        public string TagName { get; set; }
        public int UseCount { get; set; }
    }
}
