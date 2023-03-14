using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
using WP.Repository.Interfaces;
using WP.Utillities.Utilities;

namespace WP.Business.Classes
{
    public class SubscriptionBusiness : ISubscriptionBusiness
    {
        #region Consts
        private readonly ISubscriptionRepository _subscriptionRepository;
        public SubscriptionBusiness(ISubscriptionRepository subscriptionRepository)
        {
            this._subscriptionRepository = subscriptionRepository;
        }
        #endregion
        public async Task<bool> SubscribePage(SubscriptionModel subscription)
        {
            try
            {
                return await this._subscriptionRepository.SubscribePage(subscription);
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return false;
            }
        }
        public async Task<bool> RemoveSubscribePage(SubscriptionModel subscription)
        {
            try
            {
                return await this._subscriptionRepository.RemoveSubscribePage(subscription);
            }
            catch (Exception ex)
            {
                await LogManager.Log(ex);
                return false;
            }
        }
    }
}
