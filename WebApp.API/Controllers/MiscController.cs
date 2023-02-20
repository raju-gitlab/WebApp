using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WP.Business.Interfaces.Misc;
using WP.Utillities.Cachecontrol;

namespace WebApp.API.Controllers
{
    public class MiscController : ApiController
    {
        #region Consts
        private readonly IMiscBusiness _miscBusiness;
        public MiscController(IMiscBusiness miscBusiness)
        {
            this._miscBusiness = miscBusiness;
        }
        #endregion

        [HttpGet]
        public async Task<IHttpActionResult> ListPrivacies()
        {
            var result = await this._miscBusiness.ListPrivacies();
            if(result.Count != 0)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpGet]
        public async Task<IHttpActionResult> ListCategories()
        {
            var result = await this._miscBusiness.ListCategories();
            return Ok(result);
        }
    }
}
