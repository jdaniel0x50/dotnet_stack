using System.ComponentModel.DataAnnotations;


namespace form_submission.Models
{
    public abstract class BaseEntity{}
    public class User : BaseEntity
    {
        [Display(Name = "First name")]
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$", ErrorMessage = "First Name may only contain letters, spaces, and special characters '-.")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$", ErrorMessage = "Last Name may only contain letters, spaces, and special characters '-.")]
        public string LastName { get; set; }
        [Display(Name = "Age")]
        [Required]
        [Range(1,150, ErrorMessage = "Age must be between 1 and 150")]
        public int Age { get; set; }
        [Display(Name = "Email")]
        [Required]
        [EmailAddress(ErrorMessage = "The email address is not in the proper format: address@domain.ext")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}