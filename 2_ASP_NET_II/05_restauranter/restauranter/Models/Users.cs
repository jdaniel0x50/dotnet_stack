using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace restauranter.Models
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Review> Reviews { get; set; }

        // Default Constructor without parameters
        public User()
        {
            Reviews = new List<Review>();
        }
        
        // Constructor based on UserRegViewModel
        public User(UserRegViewModel RegUser)
        {
            this.Username = RegUser.Username;
            this.FirstName = RegUser.FirstName;
            this.LastName = RegUser.LastName;
            this.Birthdate = RegUser.Birthdate;
            this.Email = RegUser.Email;
            this.Password = RegUser.Password;
            this.Salt = "xyz";
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            Reviews = new List<Review>();
        }
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