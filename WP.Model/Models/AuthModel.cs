﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WP.Model.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AuthModel : RegisterModel
    {
        public string PasswordSalt { get; set; }
        public long ContactNumber { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UserTypeId { get; set; }
        public string UUID { get; set; }
    }
    public class AuthModifyModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UserId { get; set; }
    }
    public class AccountModifyModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public long ContactNumber { get; set; }
        public string UserId { get; set; }
    }
    public class UserInfoModel : AccountModifyModel{ }
}