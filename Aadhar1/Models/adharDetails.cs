using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Aadhar1.Models
{
    public class adharDetails
    {
         public int? ID { get; set; }

       
        [Required(ErrorMessage = "Firstname is required")]
        [StringLength(maximumLength: 30, MinimumLength = 4, ErrorMessage = "max length is 20 and min length is 4")]
        public string FullName { get; set; }
        


        [Required(ErrorMessage = "DOF is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Birth")]
        public string DOF { get; set; }


        //[Required(ErrorMessage = "Email is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email")]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        [StringLength(100)]
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
       

        public string ErrorMessage { get; set; }
        //public decimal filesize { get; set; }
        public string UploadUserFile(HttpPostedFileBase Image)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "png", "jpeg"};
                var fileExt = System.IO.Path.GetExtension(Image.FileName).Substring(1).ToLower();
                if (!supportedTypes.Contains(fileExt))
                {
                    ErrorMessage = "File Extension Is InValid - Only Upload jpg/png/jpeg file";
                    return ErrorMessage;
                }
                //else if (Image.ContentLength > (filesize * 1024 ))
                //{
                //    ErrorMessage = "File size Should Be UpTo " + filesize + "KB";


                //    return ErrorMessage;
                //}
                else
                {
                    //ErrorMessage = "Image Is Successfully Uploaded";
                    ErrorMessage = "";
                    return ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Upload Container Should Not Be Empty or Contact Admin";
                return ErrorMessage;
            }
        }
    }
}