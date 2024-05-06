using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Aadhar1.Models
{
    public class signup
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(maximumLength: 30, MinimumLength = 4, ErrorMessage = "max length is 20 and min length is 4")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        //[RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|" +
        //    "(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
        //    ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), " +
        //    "lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Pass", ErrorMessage ="Confirm password not match,Type again !")]
        public string ConPass { get; set; }
    }
}