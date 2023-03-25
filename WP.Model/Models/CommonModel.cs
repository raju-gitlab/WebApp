using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Utilities;

namespace WP.Model.Models
{
    public class CommonModel : ListIds
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UniqueTags { get; set; }
    }
}