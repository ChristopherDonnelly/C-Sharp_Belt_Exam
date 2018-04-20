using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace C_Sharp_Belt.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "First Name is required!")]
        [MinLength(2, ErrorMessage = "First Name must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters!")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "Last Name is required!")]
        [MinLength(2, ErrorMessage = "Last Name must contain at least 2 characters!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters!")]
        public string LastName { get; set; }
 
        [Display(Name = "Email: ")]
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage="Please ensure that you have entered a valid email address!")]
        public string Email { get; set; }
 
        //EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters!")]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z]).*$", ErrorMessage = "Password must contain at least 1 number, 1 letter, and a special character!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Display(Name = "Passwrod Confirm: ")]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string PasswordConfirmation { get; set; }

        [Range(0, 0, ErrorMessage = "Email already exists, please use unique email or login!")]
        public int Unique { get; set; }

        public User createUser(BeltExamContext _context){
            
            User newUser = new User{
                FirstName = this.FirstName,
                LastName = this.LastName,
                Email = this.Email,
                Password = this.Password
            };

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
            
            _context.users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
    }
}