using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace restauranter.Models
{
    public class Restaurant : BaseEntity
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Review> Reviews { get; set; }

        // Default Constructor without parameters
        public Restaurant()
        {
            Reviews = new List<Review>();
        }
        public Restaurant(RestaurantViewModel NewRest)
        {
            this.Name = NewRest.Name;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            Reviews = new List<Review>();
        }
    }

    public class Review : BaseEntity
    {
        public int ReviewId { get; set; }
        public User User { get; set; }
        public Restaurant Restaurant { get; set; }
        public string ReviewDescription { get; set; }
        public int StarRating { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Default Constructor without parameters
        public Review() { }
        public Review(ReviewViewModel NewReview, User CurrUser, Restaurant SelectedRestaurant)
        {
            this.User = CurrUser;
            this.Restaurant = SelectedRestaurant;
            this.ReviewDescription = NewReview.ReviewDescription;
            this.StarRating = NewReview.StarRating;
            this.VisitDate = NewReview.VisitDate;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }

    public class RestaurantViewModel : BaseEntity
    {
        [Display(Name = "Restaurant")]
        [Required]
        [MinLength(2, ErrorMessage = "Restaurant names must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string Name { get; set; }
    }


    public class ReviewViewModel : BaseEntity
    {
        [Display(Name = "Restaurant")]
        [Required]
        public string Restaurant { get; set; }
        [Display(Name = "Description")]
        [Required]
        [MinLength(10, ErrorMessage = "Reviews must have at least 10 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_ALL_EXCEPT_SENSITIVE, ErrorMessage = Constants.REGEX_ALL_MESSAGE)]
        public string ReviewDescription { get; set; }
        [Display(Name = "Rating")]
        [Required]
        [Range(1, 5)]
        public int StarRating { get; set; }
        [Display(Name = "Date of Visit")]
        [DataType(DataType.DateTime, ErrorMessage = "This is not a correctly formatted date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisitDate { get; set; }

    }
}
