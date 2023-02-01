using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.API.Controllers
{
    public class PostsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult test([FromUri]string[] str)
        {
            return Ok("");
        }
           
    }
}
