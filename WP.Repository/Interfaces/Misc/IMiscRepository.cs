using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Utilities;

namespace WP.Repository.Interfaces.Misc
{
    public interface IMiscRepository
    {
        Task<List<PrivacyModel>> ListPrivacies();
        Task<List<CategoryModel>> ListCategories();
    }
}
