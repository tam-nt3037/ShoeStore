using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Week02.Models
{
    public class CheckoutViewModel
    {
        [Key]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not correct. Ex: abc@gmail.com")]
        [Required(ErrorMessage = "You should enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should input your full name")]
        [Display(Name = "Full name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "You should input your address")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Address { get; set; }

        [Required(ErrorMessage = "You should input your date of birth")]
        [DataType(DataType.Date)]
        public string DoB { get; set; }

        [Required(ErrorMessage = "You should input your phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public bool Check { get; set; }

        [Compare( "Check"  , ErrorMessage = "You should check this term")]
        public bool isCheckTerm { get; set; }

        [Compare("Check", ErrorMessage = "You should confirm this box")]
        public bool isCheckInformation { get; set; }
    }
}