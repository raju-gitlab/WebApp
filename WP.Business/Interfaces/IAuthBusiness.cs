using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Business.Interfaces
{
    public interface IAuthBusiness
    {
        Task<bool> IsValid(string EmailId);
        Task<string> Register(AuthModel auth);
        Task<bool> Login(string username, string password);
    }
}
