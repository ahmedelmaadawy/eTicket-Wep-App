using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Full Name Is Required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address Is Required")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords DO NOT Match")]
        public string ConfirmPassword { get; set; }
    }
}
