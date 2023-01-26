using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WP.Business.Interfaces;
using WP.Model.Models;

namespace WebApp.API.Controllers
{
    public class PostsController : ApiController
    {
        #region Consts
        private readonly IPostsBusiness _postsBusiness;
        public PostsController(IPostsBusiness postsBusiness)
        {
            this._postsBusiness = postsBusiness;
        }
        #endregion

        #region POST
        [HttpPost]
        public async Task<IHttpActionResult> CreatePage([FromBody] PageModel pages)
        {
            
        }
        #endregion
    }
}
