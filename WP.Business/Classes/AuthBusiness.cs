using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Repository.Interfaces;

namespace WP.Business.Classes
{
    public class AuthBusiness : IAuthBusiness
    {
        #region Consts
        private readonly IAuthRepository _authRepository;
        public AuthBusiness(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        #endregion
        public bool isSuccess()
        {
            return this._authRepository.Issuccess();
        }
    }
}
