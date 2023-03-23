﻿using System;
using System.IO;
using System.Linq;
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
        #region Create page post
        [HttpPost]
        public async Task<IHttpActionResult> CreatePagePost(PostsViewModel post)
        {
            var result = await this._postsBusiness.CreatePagePost(post);
            if(!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion

        #region Add Post
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

        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            string result = "";
            try
            {
                string fileName = null;
                string imageName = null;
                var httpRequest = HttpContext.Current.Request;
                var postedImage = httpRequest.Files["ImageUpload"];
                var postedFile = httpRequest.Files["FileUpload"];
                var UserName = httpRequest.Form["UserName"];
                if (postedImage != null)
                {
                    imageName = new String(Path.GetFileNameWithoutExtension(postedImage.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedImage.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/" + imageName);
                    postedImage.SaveAs(filePath);
                }
                if (postedFile != null)
                {
                    fileName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/" + fileName);
                    postedFile.SaveAs(filePath);
                }
                var Image = imageName;
                var DocFile = fileName;
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(result);
        }
    }
}