using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAPP.ViewModel
{
    public class LoginUserViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

    }
}
