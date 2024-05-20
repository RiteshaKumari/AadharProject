using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aadhar1.Models
{
    public class welcome
    {
        public int? ID { get; set; }


        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(maximumLength: 30, MinimumLength = 4, ErrorMessage = "max length is 20 and min length is 4")]
        public string FullName { get; set; }



        [Required(ErrorMessage = "DOF is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public string DOF { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [StringLength(300)]
        [Required(ErrorMessage = "StreetAdd is required")]
        public string StreetAdd { get; set; }

        [Required(ErrorMessage = "ZIP or Postal Address is required")]
        public string ZIP { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("(?:m|M|male|Male|f|F|female|Female|FEMALE|MALE|Not prefer to say)$")]
        public string Gender { get; set; }

        [Display(Name = "Enter your image.")]
        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
    }
}