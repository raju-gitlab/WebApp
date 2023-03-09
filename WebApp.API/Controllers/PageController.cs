using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
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
                if(result != null || result.Count != 0)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("No Result Found");
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return BadRequest("Error in response");
            }
        }
        #endregion
        #endregion

        #region POST
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

        #region PUT
        [HttpPut]
        public async Task<IHttpActionResult> UpdatePageinfo([FromBody]PageModifyModel page)
        {
            try
            {
                var result = await this._postsBusiness.ModifyPage(page);
                if(result != null)
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
    }
}