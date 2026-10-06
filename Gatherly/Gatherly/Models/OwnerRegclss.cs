using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class OwnerRegclss
    {
        [Required(ErrorMessage = "Enter the Name")]
        public string name { set; get; }
        [EmailAddress(ErrorMessage = "Enter valid email id")]
        public string email { get; set; }
        [Required(ErrorMessage = "Enter the Phone")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Enter Valid Number")]
        public string phone { get; set; }   
        [Required(ErrorMessage = "Enter the Address")]
        public string address { set; get; }
        public string photo { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
        [Compare("pass", ErrorMessage = "Password Mismatch")]
        public string cpassword { get; set; }
        public string ownermsg { get; set; }
    }
}