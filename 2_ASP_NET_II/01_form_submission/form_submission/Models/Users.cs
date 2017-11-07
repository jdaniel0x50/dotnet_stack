using System.ComponentModel.DataAnnotations;


namespace form_submission.Models
{
    public abstract class BaseEntity{}
    public class User : BaseEntity
    {
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z''-'\.\s]+$")]
        public string LastName { get; set; }
        [Required]
        [Range(1,150)]
        public int Age { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}