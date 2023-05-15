using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Data.Enum;
using WebApplication3.Models;

namespace WebApplication3.ViewModel
{   
    //we create theview omdel even for edit it wiil make it easy to controll
    public class EditClubViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }

        public ClubCategory ClubCategory { get; set; }


    }
}
