using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoctorAPP.ViewModel
{
    public class DoctorViewModel
    {
        [Required]
        [DisplayName("First Name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Specialization")]
        [MaxLength(4000)]
        public string Specialization { get; set; }
    }
}
