using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeEF.Models
{
    public class UserModel
    {
        public int userId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime dateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string gender { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50)]
        public string city { get; set; }
    }
}
