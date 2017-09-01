﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebManager.Models
{
    public class LoginModel
    {
        [Required]
        public string  UserName { get; set; }
        [Required]
        public string UserPwd { get; set; }
        [Required]
        public string VerifyCode { get; set; }
    }
}