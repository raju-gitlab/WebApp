using System;
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

        #region Get
        #region Lists Posts
        [HttpGet]
        public async Task<IHttpActionResult> ListPosts()
        {
            try
            {
                var result = await this._postsBusiness.GetAllPosts();
                if (result != null)
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
        #endregion
        #endregion

        #region Post
        #region AddPost1
        [HttpPost]
        public async Task<IHttpActionResult> AddPost1(HttpPostedFile filename)
        {
            return Ok();
        }
        #endregion

        #region Add Page Post
        [HttpPost]
        public async Task<IHttpActionResult> AddPost([FromBody] PostsViewModel posts)
        {
            try
            {
                string result = await this._postsBusiness.CreatePost(posts);
                if (!String.IsNullOrEmpty(result))
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
        #endregion

        #region Update Post
        [HttpPost]
        public IHttpActionResult UpdatePost(HttpPostedFileBase test)
        {
            return Ok("");
        } 
        #endregion
        #endregion
    }
}
