using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Model.Models;

namespace WP.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> IsValid(string EmailId);
        Task<string> Register(RegisterModel auth);
        Task<int> Login(string username, string password);
        Task<bool> UpdatePassword(AuthModifyModel authModify);
        Task<bool> UpdateAccountDeatails(AccountModifyModel accountModify);
    }
}