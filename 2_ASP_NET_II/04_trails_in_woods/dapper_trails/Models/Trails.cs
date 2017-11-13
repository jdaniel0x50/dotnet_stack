using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace dapper_trails.Models
{
    public class Trail : BaseEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public float length { get; set; }
        public int elevation_change { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class TrailAddViewModel : BaseEntity
    {
        [Display(Name = "Trail Name")]
        [Required]
        [MinLength(2, ErrorMessage = "Trail names must have at least 2 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string name { get; set; }

        [Display(Name = "Description")]
        [MinLength(10, ErrorMessage = "Trail descriptions must have at least 10 characters")]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_ALL_EXCEPT_SENSITIVE, ErrorMessage = Constants.REGEX_ALL_MESSAGE)]
        public string description { get; set; }
        [Display(Name = "Length")]
        [Range(0,5000)]
        public float length { get; set; }
        [Display(Name = "Elevation Change")]
        [Range(-30000, 30000)]
        public int elevation_change { get; set; }
        [Display(Name = "Latitude")]
        [RegularExpression(Constants.REGEX_LATITUDE, ErrorMessage = Constants.REGEX_LATITUDE_MESSAGE)]
        public string latitude { get; set; }
        [Display(Name = "Longitude")]
        [RegularExpression(Constants.REGEX_LONGITUDE, ErrorMessage = Constants.REGEX_LONGITUDE_MESSAGE)]
        public string longitude { get; set; }
    }
}