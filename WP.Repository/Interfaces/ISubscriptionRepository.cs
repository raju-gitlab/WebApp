using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<bool> SubscribePage(SubscriptionModel subscription);
        Task<bool> RemoveSubscribePage(SubscriptionModel subscription);
    }
}
