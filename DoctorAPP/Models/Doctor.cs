using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace DoctorAPP.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

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

        public Doctor() { }

        public Doctor(int doctorId, string firstName, string lastName, string specialization)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
        }
    }
}
