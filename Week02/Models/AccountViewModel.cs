using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Week02.Models
{
    public class LoginFormModel
    {
        [Key]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not correct. Ex: abc@gmail.com")]
        [Required(ErrorMessage = "You should enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should enter your password")]
        public string Password { get; set; }
        
    }
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not correct. Ex: abc@gmail.com")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Enter your full name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string NameCus { get; set; }

        [Required(ErrorMessage = "Enter your number phone")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumberCus { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Address")]
        public string AddressCus { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Format date is wrong !")]
        [Display(Name = "doB")]
        public string DateCus { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class DetailAccountViewModel
    {
        
        
        [Required(ErrorMessage = "Enter your full name !!!")]
        [StringLength(100, ErrorMessage = "Input box {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string NameCus { get; set; }

        [Required(ErrorMessage = "Enter your number phone !!!")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumberCus { get; set; }

        [Required(ErrorMessage = "Enter your address !!!")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Input box {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name ="Address")]
        public string AddressCus { get; set; }

        [Required(ErrorMessage = "Enter your birthday !!!")]
        [DataType(DataType.Date, ErrorMessage ="Format date is wrong !")]
        [Display(Name ="Birthday")]
        public string DateCus { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    public class ForgotAccountViewModel {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not correct. Ex: abc@gmail.com")]
        public string Email { get; set; }
    }
}