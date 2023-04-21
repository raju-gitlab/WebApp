using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using WebGrease.Css.Visitor;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Utillities.Encryption;

namespace WebApp.API.Controllers
{
    public class AuthController : ApiController
    {
        #region Consts
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusiness)
        {
            this._authBusiness = authBusiness;
        }
        #endregion

        #region Get

        #region Login
        [HttpGet]
        public async Task<IHttpActionResult> Login([FromUri] string UserName, [FromUri] string Password)
        {
            var result = await this._authBusiness.Login(UserName, Password);
            if (result != null)
            {
                if(Guid.TryParse(result.ToString(), out _))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (result.ToLower().ToString() == "User not found".ToLower().ToString())
            {
                return NotFound();
            }
            else
            {
                return Ok("Password or UserName is not matced");
            }
        }
        #endregion

        #region Profile (Not Completed)
        public async Task<IHttpActionResult> YourProfile()
        {
            try
            {
                string UserId = HttpContext.Current.Request.Headers.Get("UserId").ToString();
                return Ok();
            } 
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #endregion

        #region POST
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]AuthModel auth)
        {
            if(ModelState.IsValid)
            {
                var str = await this._authBusiness.Register(auth);
                if(str != null)
                {
                    return Ok(str);
                }
                else
                {
                    return BadRequest("User Already Available");
                }
            }
            else
            {
                return BadRequest("Input Missing");
            }
        }
        #endregion
    }
}