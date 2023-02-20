using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WP.Business.Interfaces;
using WP.Model.Models;

namespace WebApp.API.Controllers
{
    public class PostsController : ApiController
    {
        #region Consts
        private readonly IPostsBusiness _postsBusiness;
        public PostsController()
        {

        }
        public PostsController(IPostsBusiness postsBusiness)
        {
            this._postsBusiness = postsBusiness;
        }
        #endregion

        [HttpGet]
        public async Task<IHttpActionResult> ListPosts()
        {
            try
            {
                var result = await this._postsBusiness.GetAllPosts();
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Bad Request");
                throw;
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AddPost(PostsViewModel posts)
        {
            try
            {
                string result = await this._postsBusiness.CreatePost(posts);
                if(!String.IsNullOrEmpty(result))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request");
                }
             }
            catch (Exception ex)
            {
                return BadRequest("Bad Request");
                throw;
            }
        }
        [HttpPost]
        public IHttpActionResult UpdatePost(HttpPostedFileBase test)
        {
            return Ok("");
        }
    }
}
