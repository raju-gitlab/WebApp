using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WP.Business.Interfaces;
using WP.Model.Models;
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
        #region GET
        public async Task<bool> IsValid(string EmailId)
        {
            try
            {
                if(!string.IsNullOrEmpty(EmailId))
                {
                    if (await this._authRepository.IsValid(EmailId)) {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    return await this._authRepository.Login(username, password);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;

            }
        }

        public async Task<string> Register(RegisterModel auth)
        {
            try
            {
                if (!await this._authRepository.IsValid(auth.Email))
                {
                    return await this._authRepository.Register(auth);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        #endregion

    }
}
