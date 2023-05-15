using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Data.Enum;
using WebApplication3.Models;

namespace WebApplication3.ViewModel
{
    //when the view getting more complecated it is beter to create a viewModel
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
        public string AppUserId { get; set; }


    }
}
