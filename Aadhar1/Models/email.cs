using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aadhar1.Models
{
    public class email
    {
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Invalid Email")]

        public string Email { get; set; }
        
    }
}