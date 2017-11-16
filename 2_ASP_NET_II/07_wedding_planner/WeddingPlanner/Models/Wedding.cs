using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
        public int WeddingId { get; set; }
        public string Wedder1 { get; set; }
        public string Wedder2 { get; set; }
        public DateTime WeddingDate { get; set; }
        public string Address { get; set; }
        public int CreatedById { get; set; }
        public User CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Default Constructor without parameters
        public Wedding()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public Wedding(WeddingViewModel NewWedding, User _currUser)
        {
            this.Wedder1 = NewWedding.Wedder1;
            this.Wedder2 = NewWedding.Wedder2;
            this.WeddingDate = NewWedding.WeddingDate;
            this.Address = NewWedding.Address;
            this.CreatedById = _currUser.UserId;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }

    public class WeddingGuest : BaseEntity
    {
        public int WeddingGuestId { get; set; }
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }
        public int UserId { get; set; }
        public User Guest { get; set; }
    }

    public class WeddingViewModel : BaseEntity
    {
        [Display(Name = "Wedding Member One")]
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string Wedder1 { get; set; }

        [Display(Name = "Wedding Member Two")]
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_NAMES, ErrorMessage = Constants.REGEX_NAMES_MESSAGE)]
        public string Wedder2 { get; set; }

        [Display(Name = "Date")]
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "This is not a correctly formatted date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CheckDateAfterToday]
        public DateTime WeddingDate { get; set; }

        [Display(Name = "Address")]
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        [RegularExpression(Constants.REGEX_ALL_EXCEPT_SENSITIVE, ErrorMessage = Constants.REGEX_ALL_MESSAGE)]
        public string Address { get; set; }
    }

    public class WeddingGuestsLineView : BaseEntity
    {
        public int WeddingId { get; set; }
        public string WeddingParty { get; set; }
        public DateTime WeddingDate { get; set; }
        public int NumGuests { get; set; }
        public bool Attending { get; set; }
        public bool MyWeddingCreation { get; set; }

        public WeddingGuestsLineView(Wedding _wedding, User _currUser, List<WeddingGuest> _allGuests)
        {
            this.WeddingId = _wedding.WeddingId;
            this.WeddingParty = _wedding.Wedder1 + " and " + _wedding.Wedder2;
            this.WeddingDate = _wedding.WeddingDate;
            
            // filter the list to include only the guests attending the currently selected wedding
            List<WeddingGuest> _thisWeddingGuests = _allGuests.Where(item => item.WeddingId == _wedding.WeddingId).ToList();
            // determine if the current user is in this wedding's guest list
            List<WeddingGuest> _userAtWedding = _thisWeddingGuests.Where(item => item.UserId == _currUser.UserId).ToList();

            this.NumGuests = _thisWeddingGuests.Count;
            if (_userAtWedding.Count > 0)
            {
                // user is on the wedding guest list
                this.Attending = true;
            }
            else
            {
                this.Attending = false;
            }

            // determine if the current user created the wedding
            if (_wedding.CreatedById == _currUser.UserId)
            {
                // user created the wedding
                this.MyWeddingCreation = true;
            }
            else
            {
                this.MyWeddingCreation = false;
            }
        }
    }

    public class AllWeddingsListViewModel : BaseEntity
    {
        public List<WeddingGuestsLineView> WeddingLines { get; set; }

        // default constructor for all weddings view
        public AllWeddingsListViewModel(List<Wedding> _allWeddings, User _currUser, List<WeddingGuest> _allGuests)
        {
            this.WeddingLines = new List<WeddingGuestsLineView>();
            foreach (Wedding _wedding in _allWeddings)
            {
                WeddingGuestsLineView _newWeddingLine = new WeddingGuestsLineView(_wedding, _currUser, _allGuests);
                this.WeddingLines.Add(_newWeddingLine);
            }
        }
    }
}
