using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShop.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression(@"(\S\D)+", ErrorMessage = " Enter User Name without spaces and numbers")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        [RegularExpression(@"^(?=.*\d).{4,8}$", ErrorMessage = "Password must be 4 to 8 characters long and contains one number")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Date Of Birth")]
        //[DataType(DataType.Date)]
        [ValidBirthDate(ErrorMessage = "Enter Birth Date in Range")]        
        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please Select One")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Provide Contact number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Contact number")]
        public string Contact { get; set; }


        [Required(ErrorMessage = "Enter Address")]
        [RegularExpression(@"[0-9a-zA-Z #,-]+")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please Select City")]
        public int CityId { get; set; }

        public IEnumerable<State> States { get; set; }



    }

}