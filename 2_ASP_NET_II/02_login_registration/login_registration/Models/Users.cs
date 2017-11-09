using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace login_registration.Models
{
    public abstract class BaseEntity { }
    public class User : BaseEntity
    {
        [Display(Name = "Username")]
        [Required]
        [MinLength(2, ErrorMessage="Username must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-Z0-9_''-'\.\s]+$", ErrorMessage = "Username may only contain letters, spaces, and special characters '_-.")]
        public string Username { get; set; }
        [Display(Name = "First name")]
        [Required]
        [MinLength(2, ErrorMessage = "First Name must have at least 2 characters")]
        [MaxLength(45)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$", ErrorMessage = "First Name may only contain letters, spaces, and special characters '-.")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        [MinLength(2, ErrorMessage = "Last Name must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$", ErrorMessage = "Last Name may only contain letters, spaces, and special characters '-.")]
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
}