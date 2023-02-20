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
        [HttpGet]
        public async Task<IHttpActionResult> PageById([FromUri]string PageId)
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

        #region POST
        [HttpPost]
        public async Task<IHttpActionResult> CreatePage([FromBody] PageModel pages)
        {
            try
            {
                pages.OwnerId = HttpContext.Current.Request.Headers.Get("OwnerId").ToString();
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