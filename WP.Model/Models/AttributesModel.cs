using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Model.Models
{
    public class AttributesModel
    {
        
    }

    public class PostsCategoryModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryUUID { get; set; }
    }
    public class PrivacyCategoryModel
    {
        public int Id { get; set; }
        public string PrivacyType { get; set; }
        public string PrivacyUUID { get; set; }
    }
}
