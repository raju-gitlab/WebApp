using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Utilities;

namespace WP.Business.Interfaces.Misc
{
    public interface IMiscBusiness
    {
        Task<List<PrivacyModel>> ListPrivacies();
        Task<List<CategoryModel>> ListCategories();
        Task<List<string>> ListTags();
        Task<bool> Addtag(string[] tags);
    }
}
