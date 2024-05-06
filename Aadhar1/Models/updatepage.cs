using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Aadhar1.Models
{
    public class updatepage
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(maximumLength: 30, MinimumLength = 4, ErrorMessage = "max length is 20 and min length is 4")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "*")]
        //[EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        //[RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain any of 3 from 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "*")]
        [Compare("Pass")]
        [DataType(DataType.Password)]
        [Display(Name = "Conform Password")]
        public string ConPass { get; set; }

    }
}