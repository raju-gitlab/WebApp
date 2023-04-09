using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Management;
using System.Web.Optimization;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Utillities.Utilities;

namespace WebApp.API.Controllers
{
    public class PageController : ApiController
    {
        #region Consts
        private readonly IPagesBusiness _postsBusiness;
        public PageController(IPagesBusiness postsBusiness)
        {
            this._postsBusiness = postsBusiness;
        }
        #endregion

        #region Get
        #region GetRoles
        [HttpGet]
        public async Task<IHttpActionResult> GetRolesList()
        {
            try
            {
                var result = await this._postsBusiness.UserRoles();
                return Ok(result);
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                throw;
            }
        }
        #endregion

        #region AllPages
        [HttpGet]
        public async Task<IHttpActionResult> AllPages()
        {
            try
            {
                List<PageModel> result = await this._postsBusiness.ListPages();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Pages
        [HttpGet]
        public async Task<IHttpActionResult> Pages(string[] category)
        {
            try
            {
                List<PageModel> result = await this._postsBusiness.ListPagesbyfilter(category);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region PageById
        [HttpGet]
        public async Task<IHttpActionResult> PageById([FromUri] string PageId)
        {
            try
            {
                var result = await this._postsBusiness.PageById(PageId);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region User Page list
        [HttpGet]
        public async Task<IHttpActionResult> PagesById()
        {
            try
            {
                string UserId = HttpContext.Current.Request.Headers.Get("UserId").ToString();
                var result = await this._postsBusiness.GetPagesByUserId(UserId);
                if (result == null || result.Count == 0)
                {
                    return Ok("No Result Found");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return BadRequest("Error in response");
            }
        }
        #endregion

        #region PageDetailsByAuthPageUsers
        public async Task<IHttpActionResult> PageDetails()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return BadRequest();
            }
        }
        #endregion

        #endregion

        #region POST

        #region CreatePage
        [HttpPost]
        public async Task<IHttpActionResult> CreatePage([FromBody] PageModel pages)
        {
            try
            {
                pages.OwnerId = HttpContext.Current.Request.Headers.Get("userid").ToString();
                string result = await this._postsBusiness.CreatePage(pages);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("NULL");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #endregion

        #region Put

        #region UpdatePageinfo
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePageinfo([FromBody] PageModifyModel page)
        {
            try
            {
                var result = await this._postsBusiness.ModifyPage(page);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Details Not updated");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Update Modifier for page
        public async Task<IHttpActionResult> UpdateModifierForPage(PageUserModel pageUser)
        {
            try
            {
                var result = await this._postsBusiness.UpdateModifierForPage(pageUser);
                if(result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                throw;
            }
        }
        #endregion

        #region Update Page Image
        [HttpPut]
        public async Task<IHttpActionResult> UploadLogo([FromBody] PageLogoModel pageLogo)
        {
            try
            {
                if (await this._postsBusiness.UploadLogo(pageLogo))
                {
                    return Ok("success");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return BadRequest(ex.InnerException.ToString());
            }
        }
        #endregion
        #endregion
    }
}