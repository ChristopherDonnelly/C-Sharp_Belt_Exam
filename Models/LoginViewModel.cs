
using System.ComponentModel.DataAnnotations;

namespace C_Sharp_Belt.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Display(Name = "Email: ")]
        [EmailAddress(ErrorMessage="Please ensure that you have entered a valid email address!")]
        public string LoginEmail { get; set; }
 
        [Display(Name = "Password: ")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
 
        [Range(0, 0, ErrorMessage = "Password does not match password on record!")]
        public int LoginPasswordConfirmation { get; set; }

        [Range(0, 0, ErrorMessage = "Email does not exist! Enter correct Email or Register.")]
        public int Found { get; set; }
    }
}