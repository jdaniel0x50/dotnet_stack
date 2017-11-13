using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace dapper_trails.Models
{
    public class User : BaseEntity
    {
        public int id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birthdate { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class UserRegViewModel : BaseEntity
    {
        [Display(Name = "Username")]
        [Required]
        [MinLength(2, ErrorMessage = "Username must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_USERNAMES, ErrorMessage = Constants.REGEX_USERNAMES_MESSAGE)]
        public string Username { get; set; }
        [Display(Name = "First name")]
        [Required]
        [MinLength(2, ErrorMessage = "First Name must have at least 2 characters")]
        [MaxLength(45)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string LastName { get; set; }
        [Display(Name = "Birthdate")]
        [DataType(DataType.DateTime, ErrorMessage = "This is not a correctly formatted date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        [Display(Name = "Email")]
        [Required]
        [MaxLength(255)]
        [EmailAddress(ErrorMessage = "The email address is not in the proper format: address@domain.ext")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
        [MaxLength(255)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class UserLoginViewModel : BaseEntity
    {
        [Display(Name = "Username")]
        [Required]
        [RegularExpression(Constants.REGEX_USERNAMES, ErrorMessage = Constants.REGEX_USERNAMES_MESSAGE)]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginRegFormModel : BaseEntity
    {
        public UserLoginViewModel loginVM { get; set; }
        public UserRegViewModel registerVM { get; set; }
    }
}