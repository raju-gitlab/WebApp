using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces.Misc;
using WP.Model.Utilities;
using WP.Repository.Interfaces.Misc;

namespace WP.Business.Classes.Misc
{
    public class MiscBusiness : IMiscBusiness
    {
        #region Consts
        private readonly IMiscRepository _miscRepository;
        public MiscBusiness(IMiscRepository miscRepository)
        {
            this._miscRepository = miscRepository;
        }
        #endregion
        public async Task<List<PrivacyModel>> ListPrivacies()
        {
            return await this._miscRepository.ListPrivacies();
        }
        public async Task<List<CategoryModel>> ListCategories()
        {
            return await this._miscRepository.ListCategories();
        }
        public async Task<List<string>> ListTags()
        {
            return await this._miscRepository.ListTags();
        }
        public async Task<bool> Addtag(string[] tags)
        {
            return await this._miscRepository.Addtag(tags);
        }
    }
}
