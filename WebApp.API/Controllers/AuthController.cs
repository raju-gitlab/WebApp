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
        [HttpGet]
        public async Task<IHttpActionResult> IsValid([FromUri]string email, [FromUri] string password)
        {
            var result =  await this._authBusiness.Login(email, password);
            return Ok(result);
        }
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
