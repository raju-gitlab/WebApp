using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;

namespace WP.Business.Classes
{
    public class PostsBusiness : IPostsBusiness
    {
        #region Consts
        private readonly IPostsRepository _postsRepository;
        public PostsBusiness(IPostsRepository postsRepository)
        {
            this._postsRepository = postsRepository;
        }
        #endregion

        #region Get
        public async Task<bool> IsValid(string pageName)
        {
            try
            {
                if (!await this._postsRepository.IsValid(pageName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Post
        public async Task<string> CreatePage(PageModel page)
        {
            try
            {
                if(await this._postsRepository.IsValid(page.PageName))
                {
                    string result = await this._postsRepository.CreatePage(page);
                    if (!string.IsNullOrEmpty(result))
                    {
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        }
        #endregion
    }
}
