﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aadhar1.Models
{
    public class emailValidation
    {
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}