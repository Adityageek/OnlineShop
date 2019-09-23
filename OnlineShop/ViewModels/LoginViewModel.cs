using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.ViewModels
{
    public class LoginViewModel 
    {
        
        
        [Required(ErrorMessage = "Enter User Name")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"(\S\D)+", ErrorMessage = "Space and numbers not allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d).{4,8}$", ErrorMessage = "Password must be 4 to 8 characters long and contains one number")]
        public string Password { get; set; }
    }
}