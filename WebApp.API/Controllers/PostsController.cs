using System;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Utillities.Utilities;

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

        #region Get Page Posts
        [HttpGet]
        public async Task<IHttpActionResult> PagePosts(string UserId, string PageId)
        {
            try
            {
                var result = await this._postsBusiness.UserPosts(UserId, PageId);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region MyRegion
        [HttpGet]
        public async Task<IHttpActionResult> TopPosts(string UserId)
        {
            try
            {
                var result = await this._postsBusiness.GetTopPostsByUserId(UserId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return BadRequest();
            }
        }
        #endregion
        #endregion

        #region Post
        #region Create page post
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        public async Task<IHttpActionResult> CreatePagePost()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var file = httpRequest.Files["UploadFile"];
                string FileName = Guid.NewGuid().ToString().Replace('-', 'a') + "." + file.ContentType.ToString().Split('/')[1];
                var posts = new PostsViewModel();
                posts.PageUUID = httpRequest.Form["PageUUID"];
                posts.UserUUID = httpRequest.Form["UserUUID"];
                posts.PostTitle = httpRequest.Form["PostTitle"];
                posts.PostDescription = httpRequest.Form["PostDescription"].ToString();
                posts.MediaVisibilityState = httpRequest.Form["MediaVisibilityState"];
                posts.PostCategoryName = httpRequest.Form["PostCategoryName"];
                posts.UserUUID = httpRequest.Form["UserUUID"];
                posts.PostTags = httpRequest.Form["AllTags"];
                posts.UniqueTags = httpRequest.Form["UniqueTags"];
                posts.FilePath = FileName;
                string result = await this._postsBusiness.CreatePagePost(posts);
                if (!String.IsNullOrEmpty(result))
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/PagePostImages/" + FileName);
                    file.SaveAs(filePath);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Bad Request");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Add Post
        [HttpPost, System.Web.Mvc.ValidateInput(false)]
        public async Task<IHttpActionResult> AddPost()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var file = httpRequest.Files["UploadFile"];
                string FileName = Guid.NewGuid().ToString().Replace('-', 'a') + "." + file.ContentType.ToString().Split('/')[1];
                var posts = new PostsViewModel();
                posts.UserUUID = httpRequest.Form["UserUUID"];
                posts.PostTitle = httpRequest.Form["PostTitle"];
                posts.PostDescription = httpRequest.Form["PostDescription"].ToString();
                posts.MediaVisibilityState = httpRequest.Form["MediaVisibilityState"];
                posts.PostCategoryName = httpRequest.Form["PostCategoryName"];
                posts.UserUUID = httpRequest.Form["UserUUID"];
                posts.PostTags = httpRequest.Form["AllTags"];
                posts.UniqueTags = httpRequest.Form["UniqueTags"];
                posts.FilePath = FileName;
                string result = await this._postsBusiness.CreatePost(posts);
                if (!String.IsNullOrEmpty(result))
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Files/PostImages/" + FileName);
                    file.SaveAs(filePath);
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
        
        #region UploadFile
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
        #endregion

        #endregion
    }
}