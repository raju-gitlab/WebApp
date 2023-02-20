﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Repository.Interfaces.Misc;

namespace WP.Business.Classes
{
    public class PagesBusiness : IPagesBusiness
    {
        #region Consts
        private readonly IPagesRepository _postsRepository;
        private readonly IMiscRepository _miscRepository;
        public PagesBusiness(IPagesRepository postsRepository, IMiscRepository miscRepository)
        {
            this._postsRepository = postsRepository;
            this._miscRepository = miscRepository;
        }
        #endregion

        #region Get
        #region Check validity
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

        #region GetAllPages
        public async Task<List<PageModel>> ListPages()
        {
            try
            {
                return await this._postsRepository.ListPages();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region PageById
        public async Task<Tuple<PageModel, List<PostsViewModel>>> PageById(string PageId)
        {
            try
            {
                var result = await this._postsRepository.PageById(PageId);
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ListPagesbyfilterMyRegion
        public async Task<List<PageModel>> ListPagesbyfilter(string[] filters)
        {
            try
            {
                return await this._postsRepository.ListPagesbyfilter(filters);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #endregion

        #region Post
        #region Create page
        public async Task<string> CreatePage(PageModel page)
        {
            try
            {
                if (!await this._postsRepository.IsValid(page.PageName))
                {
                    page.Userserialid = await this._miscRepository.GetUserId(page.OwnerId);
                    page.IdTypeTwo = await this._miscRepository.GetCategoryId(page.CategoryType);
                    page.IdTypeThree = await this._miscRepository.GetPrivacyId(page.PrivacyType);

                    if (page.Userserialid == -1)
                    {
                        return "User not found";
                    }
                    else if (page.IdTypeTwo == -1)
                    {
                        return "Category not found";

                    }
                    else if (page.IdTypeThree == -1)
                    {
                        return "Privacy type not found";
                    }
                    else
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
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new NotImplementedException();
        } 
        #endregion
        #endregion

        #region Put
        public async Task<string> ModifyPage(PageModifyModel page)
        {
            try
            {
                return await this._postsRepository.ModifyPage(page);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Delete
        #region DeletePage
        public async Task<bool> DeletePage(string UserId, string PageId)
        {
            try
            {
                if(await this._postsRepository.DeletePage(UserId, PageId))
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

        Task<Tuple<PageModel, List<PostsViewModel>>> IPagesBusiness.PageById(string PageId)
        {
            return this._postsRepository.PageById(PageId);
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
