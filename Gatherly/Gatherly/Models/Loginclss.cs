using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class Loginclss
    {
        [Required(ErrorMessage = "Enter username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string password { get; set; }

        public string loginmsg { get; set; }
    }
}