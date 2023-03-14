using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WP.Business.Interfaces;
using WP.Model.Models;

namespace WebApp.API.Controllers
{
    public class SubscriptionController : ApiController
    {
        #region Consts
        private readonly ISubscriptionBusiness _subscriptionBusiness;
        public SubscriptionController(ISubscriptionBusiness subscriptionBusiness)
        {
            this._subscriptionBusiness = subscriptionBusiness;
        }
        #endregion

        #region Post
        #region Subscription Page
        public async Task<IHttpActionResult> SubscribePage(string PageId)
        {
            try
            {
                var result = await this._subscriptionBusiness.SubscribePage(new SubscriptionModel
                {
                    UserUUID = HttpContext.Current.Request.Headers.Get("UserId").ToString(),
                    PageUUID = PageId
                });
                if (result == true)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}