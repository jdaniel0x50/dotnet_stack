namespace message_wall.Models
{
    public abstract class BaseEntity {}

    public static class Constants
    {
        public const string REGEX_ALL_EXCEPT_SENSITIVE = @"^[^'{}<>\\\u0022]+$";
        public const string REGEX_ALL_MESSAGE = @"This field can accept letters, numbers, and special characters except single quote, carat, curly and angle brackets, and backslash. Please correct and resubmit your submission.";
        public const string REGEX_USERNAMES = @"^[-_a-zA-Z0-9\.]+$";
        public const string REGEX_USERNAMES_MESSAGE = "Username may only contain letters, underscore (_), hyphen (-), and periods";
        public const string REGEX_NAMES = @"^[-a-zA-Z,\.\s]+$";
        public const string REGEX_NAMES_MESSAGE = "Name fields may only contain letters, spaces, hyphens (-), commas(,) and periods (.)";
    }
}