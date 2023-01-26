﻿using System;
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
        Task<string> Register(AuthModel auth);
        bool Login(string username, string password);
    }
}