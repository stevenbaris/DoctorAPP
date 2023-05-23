using System.ComponentModel.DataAnnotations;

namespace DoctorAPP.ViewModel
{
    public class RegisterUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{5,}$", ErrorMessage = "Must have a minimum of five characters, at least one lowercase letter, uppercase letter, numeric character and special character")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    
        [Required]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public RegisterUserViewModel()
        {
            UserName = Email;
        }
    }
}
