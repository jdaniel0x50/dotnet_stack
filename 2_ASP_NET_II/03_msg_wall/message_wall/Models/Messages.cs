using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace message_wall.Models
{
    public class Message : BaseEntity
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class MessageWithUser : Message
    {
        public string username { get; }
        public string first_name { get; }
    }

    public class Comment : BaseEntity
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int message_id { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class CommentWithUser : Comment
    {
        public string username { get; }
        public string first_name { get; }
    }

    public class MessageViewModel : BaseEntity
    {
        [Display(Name = "Message")]
        [Required]
        [MinLength(2, ErrorMessage = "Your message must have at least 2 characters")]
        [MaxLength(255, ErrorMessage = "Your message may not exceed 255 characters")]
        [RegularExpression(Constants.REGEX_ALL_EXCEPT_SENSITIVE, ErrorMessage = Constants.REGEX_ALL_MESSAGE)]
        public string message { get; set; }
    }

    public class CommentViewModel : BaseEntity
    {
        public int message_id { get; set; }
        [Display(Name = "Comment")]
        [Required]
        [MinLength(2, ErrorMessage = "Your comment must have at least 2 characters")]
        [MaxLength(255, ErrorMessage = "Your comment may not exceed 255 characters")]
        [RegularExpression(Constants.REGEX_ALL_EXCEPT_SENSITIVE, ErrorMessage = Constants.REGEX_ALL_MESSAGE)]
        public string comment { get; set; }
    }

    public class WallFormModel : BaseEntity
    {
        public MessageViewModel messageVM { get; set; }
        public CommentViewModel commentVM { get; set; }
        public MessageWithUser msgClass { get; set; }
        public CommentWithUser cmmntClass { get; set; }
    }

}