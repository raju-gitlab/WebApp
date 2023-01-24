using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WP.Business.Interfaces;

namespace WebApp.API.Controllers
{
    public class ValuesController : ApiController
    {
        #region Consts
        private readonly IAuthBusiness _authBusiness;
        public ValuesController(IAuthBusiness authBusiness)
        {
            this._authBusiness = authBusiness;
        }
        #endregion
        // GET api/values
        [HttpGet]
        public bool Get()
        
        
        {
            return _authBusiness.isSuccess(); 
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
